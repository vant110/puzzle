﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using puzzle.Data;
using puzzle.Dialogs;
using puzzle.Model;
using puzzle.Services;
using puzzle.UserControls;
using System;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using puzzle.ViewModel;
using System.Collections.Generic;
using puzzle.UserControls.Center;
using System.Media;
using MySqlConnector;

namespace puzzle
{
    public partial class MainForm : Form
    {
        private readonly Size smallFormSize = new(313, 243);
        private readonly Size normalFormSize = new(800, 600);

        public TopUC topControl;
        private BottomUC bottomControl;
        private UserControl fillControl;
        private UserControl rightControl;

        public SoundPlayer soundPlayer;
        public bool soundOn = true;

        public IList<SavedGameVM> savedGames;
        private List<sbyte> savedMethods;
        private sbyte currMethodId = 1;

        public MainForm()
        {
            InitializeComponent();

            Db.InitOptions();

            #region Top
            topControl = new TopUC
            {
                Dock = DockStyle.Top
            };
            panelTop.Controls.Add(topControl);
            #endregion
            #region Bottom
            bottomControl = new BottomUC
            {
                Dock = DockStyle.Bottom
            };
            panelBottom.Controls.Add(bottomControl);
            #endregion
            DisplayRegAndAuth();

            try
            {
                bindingSourceGallery.DataSource = new BindingList<ImageVM>(Db.LoadGallery());
            }
            catch (InvalidOperationException ex)
            when (((MySqlException)ex.InnerException).Number == 1042)
            {
                MessageBoxes.Error("База данных не доступна.");
                Close();
                return;
            }
            bindingSourceLevels.DataSource = new BindingList<LevelVM>(Db.LoadLevels());
            bindingSourcePuzzles.DataSource = new BindingList<PuzzleVM>(Db.LoadPuzzles((IList<ImageVM>)bindingSourceGallery.List));
            using (var db = new PuzzleContext(Db.Options)) {
                db.FragmentTypes.Load();
                db.AssemblyTypes.Load();
                db.CountingMethods.Load();
                bindingSourceFragmentTypes.DataSource = db.FragmentTypes.Local.ToBindingList();
                bindingSourceAssemblyTypes.DataSource = db.AssemblyTypes.Local.ToBindingList();
                bindingSourceMethods.DataSource = db.CountingMethods.Local.ToBindingList();
                db.CountingMethods.Local.Remove(db.CountingMethods.Local
                    .Where(i => i.CountingMethodId == 0)
                    .Single());
            }

            soundPlayer = new SoundPlayer(@"sounds\in-original-position.wav");
            try
            {
                soundPlayer.Load();
            }
            catch
            {
                soundPlayer = null;
                MessageBoxes.Error("Файл звукового эффекта не найден.");
            }
        }

        private static void DisposeImage(ImageAndMethodsUC right)
        {
            right.pictureBoxImage.Image?.Dispose();
            right.pictureBoxImage.Image = null;
        }

        private void ChangeFill(UserControl newFillControl)
        {
            panelCenter.Controls.Remove(fillControl);
            fillControl?.Dispose();
            fillControl = newFillControl;
            panelCenter.Controls.Add(fillControl);
            fillControl.BringToFront();
        }
        private void ChangeRight(UserControl newRightControl)
        {
            panelCenter.Controls.Remove(rightControl);
            rightControl?.Dispose();
            rightControl = newRightControl;
            panelCenter.Controls.Add(rightControl);
        }

        private void FilterLevels(ComboBox comboBoxLevels)
        {
            Debug.WriteLine(0);

            var filteredLevels = ((IList<LevelVM>)bindingSourceLevels.List)
                .Join((IList<PuzzleVM>)bindingSourcePuzzles.List,
                    l => l.Id,
                    p => p.DifficultyLevelId,
                    (l, p) => l)
                .Distinct()
                .OrderBy(i => i.Name)
                .ToList();
            bindingSourceFilteredLevels.DataSource = new BindingList<LevelVM>(filteredLevels);

            comboBoxLevels.DataSource = bindingSourceFilteredLevels.DataSource;
            comboBoxLevels.DisplayMember = "Name";
        }
        private void FilterPuzzles(Control listOrGridOrBox)
        {
            Debug.WriteLine(1);

            if (bindingSourceFilteredLevels.Current is null)
            {
                bindingSourceFilteredPuzzles.Clear();
                return;
            }

            var filteredPuzzles = ((IList<PuzzleVM>)bindingSourcePuzzles.List)
                .Where(i => i.DifficultyLevelId == ((LevelVM)bindingSourceFilteredLevels.Current).Id)
                .OrderBy(i => i.Name)
                .ToList();
            bindingSourceFilteredPuzzles.DataSource = new BindingList<PuzzleVM>(filteredPuzzles);

            if (listOrGridOrBox is ListBox listBox)
            {
                listBox.DataSource = bindingSourceFilteredPuzzles.DataSource;
            }
            else if (listOrGridOrBox is DataGridView dataGridView)
            {
                dataGridView.AutoGenerateColumns = false;
                dataGridView.DataSource = bindingSourceFilteredPuzzles.DataSource;
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    var savedGameExists = savedGames
                        .Where(p => p.PuzzleId == ((PuzzleVM)bindingSourceFilteredPuzzles[i]).Id)
                        .Any();
                    if (savedGameExists) {
                        dataGridView.Rows[i].Cells[1].Value = new Bitmap(Properties.Resources.floppy_disk);
                    }
                    else
                    {
                        dataGridView.Rows[i].Cells[1].Value = new Bitmap(1, 1);
                    }
                }
            }
            else if (listOrGridOrBox is ComboBox comboBox)
            {
                comboBox.DataSource = bindingSourceFilteredPuzzles.DataSource;
            }
        }
        private void DisplayImage(ImageAndMethodsUC right)
        {
            Debug.WriteLine(2);

            if (bindingSourceFilteredPuzzles.Position == -1)
            {
                right.pictureBoxImage.Image = null;
                return;
            }

            right.pictureBoxImage.Image = Image.FromStream(
                ((IList<ImageVM>)bindingSourceGallery.List)
                .Where(i => i.Id == ((PuzzleVM)bindingSourceFilteredPuzzles.Current).ImageId)
                .Select(i => i.Image).Single());
        }
        private void DisplaySavedGames(ImageAndMethodsUC right)
        {
            if (bindingSourceFilteredPuzzles.Position == -1)
            {
                // скрыть method panel?
                return;
            }

            savedMethods = savedGames
                .Where(i => i.PuzzleId == ((PuzzleVM)bindingSourceFilteredPuzzles.Current).Id)
                .Select(i => i.CountingMethodId).ToList();

            right.buttonSaved1.Visible = savedMethods.Contains(1);
            right.buttonSaved2.Visible = savedMethods.Contains(2);
            right.buttonSaved0.Visible = savedMethods.Contains(0);

            if (right.radioButton1.Checked)
            {
                bottomControl.ButtonLoadEnabled = savedMethods.Contains(1);
            }
            else if (right.radioButton2.Checked)
            {
                bottomControl.ButtonLoadEnabled = savedMethods.Contains(2);
            }
            else if (right.radioButton0.Checked)
            {
                bottomControl.ButtonLoadEnabled = savedMethods.Contains(0);
            }
        }

        private void ShowRecordsForm(sbyte methodId)
        {
            var form = new RecordsForm();

            FilterLevels(form.comboBoxLevel);
            form.comboBoxLevel.SelectedItem = bindingSourceFilteredLevels.Current;

            FilterPuzzles(form.comboBoxPuzzle);
            form.comboBoxPuzzle.SelectedItem = bindingSourceFilteredPuzzles.Current;

            form.comboBoxMethod.DataSource = bindingSourceMethods.DataSource;
            form.comboBoxMethod.SelectedValue = methodId;

            form.comboBoxLevel.SelectedValueChanged += new EventHandler((s, e) =>
            {
                bindingSourceFilteredLevels.Position = form.comboBoxLevel.SelectedIndex;
                FilterPuzzles(form.comboBoxPuzzle);
            });

            form.comboBoxPuzzle.SelectedValueChanged += new EventHandler((s, e) =>
            {
                bindingSourceFilteredPuzzles.Position = form.comboBoxPuzzle.SelectedIndex;
            });

            form.comboBoxMethod.SelectedValueChanged += new EventHandler((s, e) =>
            {
                bindingSourceMethods.Position = form.comboBoxMethod.SelectedIndex;
            });

            form.Show();
        }

        private Game CreateNewGame(PuzzleVM puzzle)
        {
            Image image = Image.FromStream(
                ((IList<ImageVM>)bindingSourceGallery.List)
                .Where(i => i.Id == puzzle.ImageId)
                .Select(i => i.Image)
                .Single());
            LevelVM level = ((IList<LevelVM>)bindingSourceFilteredLevels.List)
                .Where(i => i.Id == puzzle.DifficultyLevelId)
                .Single();

            Game game = new(
                level.FragmentTypeId,
                level.AssemblyTypeId,
                level.HorizontalFragmentCount,
                level.VerticalFragmentCount,
                image);
            game.FragmentNumbers = puzzle.FragmentNumbers;

            game.CountingMethodId = puzzle.CountingMethodId;
            if (game.CountingMethodId == 1)
            {
                game.Score = puzzle.Score;
            }
            else if (game.CountingMethodId == 2)
            {
                game.Time = puzzle.Time;
            }

            return game;
        }
        private Game CreateSavedGame(PuzzleVM puzzle)
        {
            SavedGameVM savedGame = savedGames
                .Where(i => i.PuzzleId == puzzle.Id 
                    && i.CountingMethodId == puzzle.CountingMethodId)
                .Single();
            Image image = Image.FromStream(
                ((IList<ImageVM>)bindingSourceGallery.List)
                .Where(i => i.Id == puzzle.ImageId)
                .Select(i => i.Image)
                .Single());
            LevelVM level = ((IList<LevelVM>)bindingSourceFilteredLevels.List)
                .Where(i => i.Id == puzzle.DifficultyLevelId)
                .Single();

            Game game = new(
                level.FragmentTypeId,
                level.AssemblyTypeId,
                level.HorizontalFragmentCount,
                level.VerticalFragmentCount,
                image);
            game.FragmentNumbers = savedGame.FieldFragmentNumbers;

            game.CountingMethodId = savedGame.CountingMethodId;
            if (game.CountingMethodId == 1)
            {
                game.Score = savedGame.Score;
            }
            else if (game.CountingMethodId == 2)
            {
                game.Time = savedGame.Time;
            }

            return game;
        }

        private void InitTopControl(ImageAndMethodsUC right, PuzzleVM puzzle)
        {
            if (right.radioButton1.Checked)
            {
                currMethodId = 1;
                puzzle.CountingMethodId = 1;
                puzzle.Score = 0;

                topControl.labelMethod.Visible = true;
                topControl.labelValue.Visible = true;

                topControl.labelMethod.Text = "Очки:";
                topControl.labelValue.Text = "0";
            }
            else if (right.radioButton2.Checked)
            {
                currMethodId = 2;
                puzzle.CountingMethodId = 2;
                puzzle.Time = 0;

                topControl.labelMethod.Visible = true;
                topControl.labelValue.Visible = true;

                topControl.labelMethod.Text = "Время:";
                topControl.labelValue.Text = "00:00:00";

            }
            else
            {
                currMethodId = 0;
                puzzle.CountingMethodId = 0;

                topControl.labelMethod.Visible = false;
                topControl.labelValue.Visible = false;
            }
        }

        internal void DisplayRegAndAuth()
        {
            Size = smallFormSize;
            CenterToScreen();

            var fill = new RegAndAuthUC(this)
            {
                Dock = DockStyle.Fill
            };
            ChangeFill(fill);
            ChangeRight(null);

            topControl.buttonBack.Visible = false;
            topControl.buttonImage.Visible = false;
            topControl.buttonSound.Visible = false;
            topControl.labelTitle.Text = "Регистрация/авторизация";
            topControl.labelMethod.Visible = false;
            topControl.labelValue.Visible = false;

            panelBottom.Hide();
        }
        internal void DisplayAdminMenu()
        {
            Size = smallFormSize;
            CenterToScreen();

            var fill = new AdminMenuUC(this)
            {
                Dock = DockStyle.Fill
            };
            ChangeFill(fill);
            ChangeRight(null);

            topControl.buttonBack.Visible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                DisplayRegAndAuth();
            });
            topControl.buttonImage.Visible = false;
            topControl.buttonSound.Visible = false;
            topControl.labelTitle.Text = "Меню администратора";
            topControl.labelMethod.Visible = false;
            topControl.labelValue.Visible = false;

            panelBottom.Hide();
        }
        internal void DisplayGallery()
        {
            Size = normalFormSize;
            CenterToScreen();
            #region Right
            var right = new ImageAndMethodsUC()
            {
                Dock = DockStyle.Right,
            };
            ChangeRight(right);
            right.panelMethods.Visible = false;
            #endregion
            #region Fill
            var fill = new ListUC()
            {
                Dock = DockStyle.Fill,
            };
            ChangeFill(fill);

            fill.panelLevel.Visible = false;

            bindingSourceGallery.ListChanged += new ListChangedEventHandler((s, e) =>
            {
                var selectedItem = (ImageVM)fill.listBox.SelectedItem;
                if (selectedItem is null)
                {
                    return;
                }

                var selectedIndex = fill.listBox.SelectedIndex;
                if (selectedIndex != bindingSourceGallery.List.Count)
                {
                    selectedItem = (ImageVM)bindingSourceGallery.List[selectedIndex];
                    right.pictureBoxImage.Image = Image.FromStream(selectedItem.Image);
                }
            });

            fill.listBox.SelectedValueChanged += new EventHandler((s, e) =>
            {
                var selectedItem = (ImageVM)fill.listBox.SelectedItem;
                if (selectedItem is null)
                {
                    return;
                }

                right.pictureBoxImage.Image = Image.FromStream(selectedItem.Image);
            });
            fill.listBox.DataSource = bindingSourceGallery.DataSource;
            #endregion
            #region Top
            topControl.buttonBack.Visible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                DisposeImage(right);
                DisplayAdminMenu();
            });
            topControl.buttonImage.Visible = false;
            topControl.buttonSound.Visible = false;
            topControl.labelTitle.Text = "Галерея";
            topControl.labelMethod.Visible = false;
            topControl.labelValue.Visible = false;
            #endregion
            #region Bottom
            panelBottom.Show();
            bottomControl.ButtonDeleteVisible = true;
            bottomControl.ButtonDeleteClick = new EventHandler((s, e) =>
            {
                var selectedItem = (ImageVM)fill.listBox.SelectedItem;
                if (selectedItem is null) return;
                try
                {
                    var id = new MySqlParameter("@id", selectedItem.Id);
                    using (var db = new PuzzleContext(Db.Options))
                    {
                        int rowsAffected = db.Database.ExecuteSqlRaw("CALL `delete_image` (@id)", id);
                        if (rowsAffected != 1)
                        {
                            throw new Exception("Ошибка.");
                        }
                    }

                    LocalStorage.Delete(selectedItem.Path);
                    DisposeImage(right);
                    bindingSourceGallery.Remove(selectedItem);
                    MessageBoxes.Info("Успешно.");
                }
                catch (MySqlException ex)
                when (ex.Number == 1451)
                {
                    MessageBoxes.Error("Невозможно удалить изображение,\nпока оно используется хотя бы в одном пазле.");
                }
                catch (Exception ex)
                {
                    MessageBoxes.Error(ex.Message);
                }
            });

            bottomControl.ButtonUpdateVisible = false;
            bottomControl.ButtonLoadVisible = false;
            bottomControl.ButtonInsertOrNewGameText = "Добавить";
            bottomControl.ButtonInsertOrNewGameClick = new EventHandler((s, e) =>
            {
                var form = new InsertImageForm();

                var newImage = new ImageVM();
                form.buttonFile.Click += new EventHandler((s, e) =>
                {
                    if (newImage.Image != null)
                    {
                        newImage.Image.Close();
                        newImage.Image = null;
                        newImage.Path = null;
                        form.labelFile.Text = "Файл не выбран";
                    }
                    using var openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Изображения (*.png)|*.png";
                    openFileDialog.RestoreDirectory = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        newImage.Image = openFileDialog.OpenFile();

                        string path = openFileDialog.SafeFileName;
                        int length = path.Length > 30 ? 30 : path.Length;
                        newImage.Path = path.Substring(0, length);

                        form.labelFile.Text = newImage.Path;
                    }
                });

                form.buttonInsert.Click += new EventHandler((s, e) =>
                {
                    try
                    {
                        newImage.Name = form.textBoxName.Text;
                        if (!Validator.IsImageName(newImage.Name))
                        {
                            throw new Exception("Название изображения некорректно.");
                        }
                        if (newImage.Image is null)
                        {
                            throw new Exception("Выберите файл изображения.");
                        }

                        newImage.Image = LocalStorage.Resize(newImage.Image);
                        newImage.ImageHash = Hasher.HashImage(newImage.Image);

                        var image = new ImageVM 
                        { 
                            Name = newImage.Name,
                            Path = newImage.Path,
                            Image = newImage.Image,
                            ImageHash = newImage.ImageHash
                        };

                        var p1 = new MySqlParameter("@p1", image.Name);
                        var p2 = new MySqlParameter("@p2", image.Path);
                        var p3 = new MySqlParameter("@p3", image.ImageHash);
                        using (var db = new PuzzleContext(Db.Options))
                        {
                            int rowsAffected = db.Database.ExecuteSqlRaw("CALL `insert_image` (@p1, @p2, @p3)", p1, p2, p3);
                            if (rowsAffected != 1)
                            {
                                throw new Exception("Ошибка.");
                            }
                            image.Id = db.Galleries
                                .Where(i => i.Name == image.Name)
                                .Select(i => i.ImageId).Single();
                        }

                        LocalStorage.Save(image.Image, image.Path);
                        //newImage.Image.Close();
                        newImage.Image = null;
                        form.labelFile.Text = "Файл не выбран";

                        bindingSourceGallery.Add(image);

                        fill.listBox.SelectedItem = image;
                        right.pictureBoxImage.Image = Image.FromStream(image.Image);

                        MessageBoxes.Info("Успешно.");
                    }
                    catch (MySqlException ex) 
                    when (ex.Number == 1062)
                    {
                        if (ex.Message.Contains("'gallery.name'"))
                        {
                            MessageBoxes.Error("Название изображения занято.");
                        }
                        else if (ex.Message.Contains("'gallery.path'"))
                        {
                            MessageBoxes.Error("Название файла занято.");
                        }
                        else
                        {
                            MessageBoxes.Error(ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxes.Error(ex.Message);
                    }
                });

                form.FormClosed += new FormClosedEventHandler((s, e) =>
                {
                    if (newImage.Image != null)
                    {
                        //newImage.Image.Close();
                        newImage.Image = null;
                    }
                });

                form.ShowDialog();
            });
            #endregion
        }
        internal void DisplayLevels()
        {
            Size = normalFormSize;
            CenterToScreen();

            #region Right
            var right = new LevelInfoUC()
            {
                Dock = DockStyle.Right
            };
            ChangeRight(right);
            #endregion
            #region Fill
            var fill = new ListUC()
            {
                Dock = DockStyle.Fill,
            };
            ChangeFill(fill);

            fill.panelLevel.Visible = false;

            right.labelHorizontal.Text = "";
            right.labelVertical.Text = "";
            right.labelFragmentType.Text = "";
            right.labelAssemblyType.Text = "";

            bindingSourceLevels.ListChanged += new ListChangedEventHandler((s, e) =>
            {
                var selectedItem = (LevelVM)fill.listBox.SelectedItem;
                if (selectedItem is null)
                {
                    return;
                }

                var selectedIndex = fill.listBox.SelectedIndex;
                if (selectedIndex != bindingSourceLevels.List.Count)
                {
                    selectedItem = (LevelVM)bindingSourceLevels.List[selectedIndex];

                    right.labelHorizontal.Text = selectedItem.HorizontalFragmentCount.ToString();
                    right.labelVertical.Text = selectedItem.VerticalFragmentCount.ToString();
                    right.labelFragmentType.Text = ((IList<FragmentType>)bindingSourceFragmentTypes.List)
                        .Where(i => i.FragmentTypeId == selectedItem.FragmentTypeId)
                        .Select(i => i.Name).Single();
                    right.labelAssemblyType.Text = ((IList<AssemblyType>)bindingSourceAssemblyTypes.List)
                        .Where(i => i.AssemblyTypeId == selectedItem.AssemblyTypeId)
                        .Select(i => i.Name).Single();
                }
            });

            fill.listBox.SelectedValueChanged += new EventHandler((s, e) =>
            {
                var selectedItem = (LevelVM)fill.listBox.SelectedItem;
                if (selectedItem is null)
                {
                    return;
                }

                right.labelHorizontal.Text = selectedItem.HorizontalFragmentCount.ToString();
                right.labelVertical.Text = selectedItem.VerticalFragmentCount.ToString();
                right.labelFragmentType.Text = ((IList<FragmentType>)bindingSourceFragmentTypes.List)
                    .Where(i => i.FragmentTypeId == selectedItem.FragmentTypeId)
                    .Select(i => i.Name).Single();
                right.labelAssemblyType.Text = ((IList<AssemblyType>)bindingSourceAssemblyTypes.List)
                    .Where(i => i.AssemblyTypeId == selectedItem.AssemblyTypeId)
                    .Select(i => i.Name).Single();
            });
            fill.listBox.DataSource = bindingSourceLevels.DataSource;
            #endregion
            #region Top
            topControl.buttonBack.Visible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                DisplayAdminMenu();
            });
            topControl.buttonImage.Visible = false;
            topControl.buttonSound.Visible = false;
            topControl.labelTitle.Text = "Уровни сложности";
            topControl.labelMethod.Visible = false;
            topControl.labelValue.Visible = false;
            #endregion
            #region Bottom
            panelBottom.Show();
            bottomControl.ButtonDeleteVisible = true;
            bottomControl.ButtonDeleteClick = new EventHandler((s, e) =>
            {
                var selectedItem = (LevelVM)fill.listBox.SelectedItem;
                if (selectedItem is null) return;
                try
                {
                    var id = new MySqlParameter("@id", selectedItem.Id);
                    using (var db = new PuzzleContext(Db.Options))
                    {
                        int rowsAffected = db.Database.ExecuteSqlRaw("CALL `delete_level` (@id)", id);
                        if (rowsAffected != 1)
                        {
                            throw new Exception("Ошибка.");
                        }
                    }

                    right.labelHorizontal.Text = "";
                    right.labelVertical.Text = "";
                    right.labelFragmentType.Text = "";
                    right.labelAssemblyType.Text = "";

                    bindingSourceLevels.Remove(selectedItem);

                    MessageBoxes.Info("Успешно.");
                }
                catch (MySqlException ex)
                when (ex.Number == 1451)
                {
                    MessageBoxes.Error("Невозможно удалить уровень сложности,\nпока он используется хотя бы в одном пазле.");
                }
                catch (Exception ex)
                {
                    MessageBoxes.Error(ex.Message);
                }
            });

            bottomControl.ButtonUpdateVisible = true;
            bottomControl.ButtonUpdateClick = new EventHandler((s, e) =>
            {
                var selectedItem = (LevelVM)fill.listBox.SelectedItem;
                if (selectedItem is null) return;
                var form = new InsertOrUpdateLevelForm(this)
                {
                    Text = "Изменение уровня сложности",
                };

                form.textBoxName.Text = selectedItem.Name;
                form.numericUpDownHorizontal.Value = selectedItem.HorizontalFragmentCount;
                form.labelVertical.Text = selectedItem.VerticalFragmentCount.ToString();
                form.comboBoxFragmentType.SelectedValue = selectedItem.FragmentTypeId;
                form.comboBoxAssemblyType.SelectedValue = selectedItem.AssemblyTypeId;
                form.buttonInsertOrUpdate.Text = "Изменить";
                form.ButtonInsertOrUpdateClick = new EventHandler((s, e) =>
                {
                    try
                    {
                        if (!Validator.IsLevelName(form.textBoxName.Text))
                        {
                            throw new Exception("Название уровня сложности некорректно.");
                        }
                        if (form.comboBoxFragmentType.SelectedItem is null)
                        {
                            throw new Exception("Выберите тип фрагментов.");
                        }
                        if (form.comboBoxAssemblyType.SelectedItem is null)
                        {
                            throw new Exception("Выберите тип сборки.");
                        }

                        var level = new LevelVM
                        {
                            Id = selectedItem.Id,
                            Name = form.textBoxName.Text,
                            HorizontalFragmentCount = (sbyte)form.numericUpDownHorizontal.Value,
                            FragmentTypeId = (sbyte)form.comboBoxFragmentType.SelectedValue,
                            AssemblyTypeId = (sbyte)form.comboBoxAssemblyType.SelectedValue
                        };

                        var id = new MySqlParameter("@id", level.Id);
                        var p1 = new MySqlParameter("@p1", level.Name);
                        var p2 = new MySqlParameter("@p2", level.HorizontalFragmentCount);
                        var p3 = new MySqlParameter("@p3", level.VerticalFragmentCount);
                        var p4 = new MySqlParameter("@p4", level.FragmentTypeId);
                        var p5 = new MySqlParameter("@p5", level.AssemblyTypeId);
                        using (var db = new PuzzleContext(Db.Options))
                        {
                            int rowsAffected = db.Database.ExecuteSqlRaw("CALL `update_level` (@id, @p1, @p2, @p3, @p4, @p5)", id, p1, p2, p3, p4, p5);
                            if (rowsAffected != 1)
                            {
                                throw new Exception("Можно изменить только название уровня сложности,\nпока он используется хотя бы в одном пазле.");
                            }
                        }

                        selectedItem.Name = level.Name;
                        selectedItem.HorizontalFragmentCount = level.HorizontalFragmentCount;
                        selectedItem.FragmentTypeId = level.FragmentTypeId;
                        selectedItem.AssemblyTypeId = level.AssemblyTypeId;
                        MessageBoxes.Info("Успешно.");
                    }
                    catch (MySqlException ex)
                    when (ex.Number == 1062)
                    {
                        MessageBoxes.Error("Название уровня сложности занято.");
                    }
                    catch (Exception ex)
                    {
                        MessageBoxes.Error(ex.Message);
                    }
                });

                form.ShowDialog();
            });

            bottomControl.ButtonLoadVisible = false;
            bottomControl.ButtonInsertOrNewGameText = "Добавить";
            bottomControl.ButtonInsertOrNewGameClick = new EventHandler((s, e) =>
            {
                var form = new InsertOrUpdateLevelForm(this)
                {
                    Text = "Добавление уровня сложности",
                };

                form.buttonInsertOrUpdate.Text = "Добавить";
                form.ButtonInsertOrUpdateClick = new EventHandler((s, e) =>
                {
                    try
                    {
                        if (!Validator.IsLevelName(form.textBoxName.Text))
                        {
                            throw new Exception("Название уровня сложности некорректно.");
                        }
                        if (form.comboBoxFragmentType.SelectedItem is null)
                        {
                            throw new Exception("Выберите тип фрагментов.");
                        }
                        if (form.comboBoxAssemblyType.SelectedItem is null)
                        {
                            throw new Exception("Выберите тип сборки.");
                        }

                        var level = new LevelVM
                        {
                            Name = form.textBoxName.Text,
                            HorizontalFragmentCount = (sbyte)form.numericUpDownHorizontal.Value,
                            FragmentTypeId = (sbyte)form.comboBoxFragmentType.SelectedValue,
                            AssemblyTypeId = (sbyte)form.comboBoxAssemblyType.SelectedValue
                        };

                        var p1 = new MySqlParameter("@p1", level.Name);
                        var p2 = new MySqlParameter("@p2", level.HorizontalFragmentCount);
                        var p3 = new MySqlParameter("@p3", level.VerticalFragmentCount);
                        var p4 = new MySqlParameter("@p4", level.FragmentTypeId);
                        var p5 = new MySqlParameter("@p5", level.AssemblyTypeId);
                        using (var db = new PuzzleContext(Db.Options))
                        {
                            int rowsAffected = db.Database.ExecuteSqlRaw("CALL `insert_level` (@p1, @p2, @p3, @p4, @p5)", p1, p2, p3, p4, p5);
                            if (rowsAffected != 1)
                            {
                                throw new Exception("Ошибка.");
                            }
                            level.Id = db.DifficultyLevels
                                .Where(i => i.Name == level.Name)
                                .Select(i => i.DifficultyLevelId).Single();
                        }

                        bindingSourceLevels.Add(level);

                        fill.listBox.SelectedItem = level;
                        var selectedItem = (LevelVM)fill.listBox.SelectedItem;
                        right.labelHorizontal.Text = selectedItem.HorizontalFragmentCount.ToString();
                        right.labelVertical.Text = selectedItem.VerticalFragmentCount.ToString();
                        right.labelFragmentType.Text = ((IList<FragmentType>)bindingSourceFragmentTypes.List)
                            .Where(i => i.FragmentTypeId == selectedItem.FragmentTypeId)
                            .Select(i => i.Name).Single();
                        right.labelAssemblyType.Text = ((IList<AssemblyType>)bindingSourceAssemblyTypes.List)
                            .Where(i => i.AssemblyTypeId == selectedItem.AssemblyTypeId)
                            .Select(i => i.Name).Single();

                        MessageBoxes.Info("Успешно.");
                    }
                    catch (MySqlException ex)
                    when (ex.Number == 1062)
                    {
                        MessageBoxes.Error("Название уровня сложности занято.");
                    }
                    catch (Exception ex)
                    {
                        MessageBoxes.Error(ex.Message);
                    }
                });

                form.ShowDialog();
            });
            #endregion
        }
        internal void DisplayPuzzles()
        {
            Size = normalFormSize;
            CenterToScreen();

            #region Right
            var right = new ImageAndMethodsUC()
            {
                Dock = DockStyle.Right
            };
            ChangeRight(right);
            right.panelMethods.Visible = false;
            #endregion
            #region Fill
            var fill = new ListUC()
            {
                Dock = DockStyle.Fill,
            };
            ChangeFill(fill);
            fill.panelLevel.Visible = true;

            fill.comboBoxLevels.SelectedValueChanged += new EventHandler((s, e) =>
            {
                bindingSourceFilteredLevels.Position = fill.comboBoxLevels.SelectedIndex;
                FilterPuzzles(fill.listBox);
            });

            fill.listBox.SelectedValueChanged += new EventHandler((s, e) =>
            {
                bindingSourceFilteredPuzzles.Position = fill.listBox.SelectedIndex;
                DisplayImage(right);
            });
            
            FilterLevels(fill.comboBoxLevels);
            #endregion
            #region Top
            topControl.buttonBack.Visible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                DisposeImage(right);
                DisplayAdminMenu();
            });
            topControl.buttonImage.Visible = false;
            topControl.buttonSound.Visible = false;
            topControl.labelTitle.Text = "Пазлы";
            topControl.labelMethod.Visible = false;
            topControl.labelValue.Visible = false;
            #endregion
            #region Bottom
            panelBottom.Show();
            bottomControl.ButtonDeleteVisible = true;
            bottomControl.ButtonDeleteClick = new EventHandler((s, e) =>
            {
                var puzzle = (PuzzleVM)fill.listBox.SelectedItem;
                if (puzzle is null) return;
                try
                {
                    var id = new MySqlParameter("@id", puzzle.Id);
                    using (var db = new PuzzleContext(Db.Options))
                    {
                        int rowsAffected = db.Database.ExecuteSqlRaw("CALL `delete_puzzle` (@id)", id);
                        if (rowsAffected != 1)
                        {
                            throw new Exception("Ошибка.");
                        }
                    }

                    var level = fill.comboBoxLevels.SelectedItem;
                    var puzzleIndex = fill.listBox.SelectedIndex;

                    bindingSourcePuzzles.Remove(puzzle);

                    FilterLevels(fill.comboBoxLevels);
                    if (bindingSourceFilteredLevels.Contains(level)) 
                    {
                        fill.comboBoxLevels.SelectedItem = level;
                        if (bindingSourceFilteredPuzzles.Count > 1) 
                        {
                            if (bindingSourceFilteredPuzzles.Count > puzzleIndex)
                            {
                                fill.listBox.SelectedIndex = puzzleIndex;
                            }                      
                            else if (bindingSourceFilteredPuzzles.Count == puzzleIndex)
                            {
                                fill.listBox.SelectedIndex = puzzleIndex - 1;
                            }
                        }
                    }
                    else if (bindingSourceFilteredLevels.Count == 0)
                    {
                        FilterPuzzles(fill.listBox);
                    }

                    MessageBoxes.Info("Успешно.");
                }
                catch (Exception ex)
                {
                    MessageBoxes.Error(ex.Message);
                }
            });

            bottomControl.ButtonUpdateVisible = true;
            bottomControl.ButtonUpdateClick = new EventHandler((s, e) =>
            {
                var puzzle = (PuzzleVM)fill.listBox.SelectedItem;
                if (puzzle is null) return;

                var form = new InsertOrUpdatePuzzleForm(this)
                {
                    Text = "Изменение пазла",
                };
                form.textBoxName.Text = puzzle.Name;
                form.comboBoxImage.SelectedValue = puzzle.ImageId;
                form.comboBoxLevel.SelectedValue = puzzle.DifficultyLevelId;

                Game.Instance = CreateNewGame(puzzle);

                form.buttonInsertOrUpdate.Text = "Изменить";
                form.ButtonInsertOrUpdateClick = new EventHandler((s, e) =>
                {
                    try
                    {
                        if (!Validator.IsPuzzleName(form.textBoxName.Text))
                        {
                            throw new Exception("Название пазла некорректно.");
                        }
                        if (form.comboBoxImage.SelectedItem is null)
                        {
                            throw new Exception("Выберите изображение.");
                        }
                        if (form.comboBoxLevel.SelectedItem is null)
                        {
                            throw new Exception("Выберите уровень сложности.");
                        }
                        if (Game.Instance is null)
                        {
                            throw new Exception("Перемешайте фрагменты.");
                        }

                        var tempPuzzle = new PuzzleVM
                        {
                            Name = form.textBoxName.Text,
                            ImageId = ((ImageVM)form.comboBoxImage.SelectedItem).Id,
                            DifficultyLevelId = ((LevelVM)form.comboBoxLevel.SelectedItem).Id,
                            FragmentNumbers = Game.Instance.FragmentNumbers
                        };

                        var id = new MySqlParameter("@id", puzzle.Id);
                        var p1 = new MySqlParameter("@p1", tempPuzzle.Name);
                        var p2 = new MySqlParameter("@p2", tempPuzzle.ImageId);
                        var p3 = new MySqlParameter("@p3", tempPuzzle.DifficultyLevelId);
                        var p4 = new MySqlParameter("@p4", tempPuzzle.FragmentNumbers);
                        using (var db = new PuzzleContext(Db.Options))
                        {
                            int rowsAffected = db.Database.ExecuteSqlRaw("CALL `update_puzzle` (@id, @p1, @p2, @p3, @p4)", id, p1, p2, p3, p4);
                            if (rowsAffected != 1)
                            {
                                throw new Exception("Ошибка.");
                            }
                        }

                        puzzle.Name = tempPuzzle.Name;
                        puzzle.ImageId = tempPuzzle.ImageId;
                        puzzle.DifficultyLevelId = tempPuzzle.DifficultyLevelId;
                        puzzle.FragmentNumbers = tempPuzzle.FragmentNumbers;

                        FilterLevels(fill.comboBoxLevels);
                        fill.comboBoxLevels.SelectedValue = puzzle.DifficultyLevelId;
                        fill.listBox.SelectedItem = puzzle;

                        MessageBoxes.Info("Успешно.");
                    }
                    catch (MySqlException ex) 
                    when (ex.Number == 1062)
                    {
                        MessageBoxes.Error("Название пазла занято.");
                    }
                    catch (Exception ex)
                    {
                        MessageBoxes.Error(ex.Message);
                    }
                });

                form.ShowDialog();
            });

            bottomControl.ButtonLoadVisible = false;
            bottomControl.ButtonInsertOrNewGameText = "Добавить";
            bottomControl.ButtonInsertOrNewGameClick = new EventHandler((s, e) =>
            {
                var form = new InsertOrUpdatePuzzleForm(this)
                {
                    Text = "Добавление пазла",
                };
                form.buttonInsertOrUpdate.Text = "Добавить";
                form.ButtonInsertOrUpdateClick = new EventHandler((s, e) =>
                {
                    try
                    {
                        if (!Validator.IsPuzzleName(form.textBoxName.Text))
                        {
                            throw new Exception("Название пазла некорректно.");
                        }
                        if (form.comboBoxImage.SelectedItem is null)
                        {
                            throw new Exception("Выберите изображение.");
                        }
                        if (form.comboBoxLevel.SelectedItem is null)
                        {
                            throw new Exception("Выберите уровень сложности.");
                        }
                        if (Game.Instance is null)
                        {
                            throw new Exception("Перемешайте фрагменты.");
                        }

                        var puzzle = new PuzzleVM
                        {
                            Name = form.textBoxName.Text,
                            ImageId = ((ImageVM)form.comboBoxImage.SelectedItem).Id,
                            DifficultyLevelId = ((LevelVM)form.comboBoxLevel.SelectedItem).Id,
                            FragmentNumbers = Game.Instance.FragmentNumbers
                        };

                        var p1 = new MySqlParameter("@p1", puzzle.Name);
                        var p2 = new MySqlParameter("@p2", puzzle.ImageId);
                        var p3 = new MySqlParameter("@p3", puzzle.DifficultyLevelId);
                        var p4 = new MySqlParameter("@p4", puzzle.FragmentNumbers);
                        using (var db = new PuzzleContext(Db.Options))
                        {
                            int rowsAffected = db.Database.ExecuteSqlRaw("CALL `insert_puzzle` (@p1, @p2, @p3, @p4)", p1, p2, p3, p4);
                            if (rowsAffected != 1)
                            {
                                throw new Exception("Ошибка.");
                            }
                            puzzle.Id = db.Puzzles
                                .Where(i => i.Name == puzzle.Name)
                                .Select(i => i.PuzzleId).Single();
                        }

                        bindingSourcePuzzles.Add(puzzle);

                        FilterLevels(fill.comboBoxLevels);
                        fill.comboBoxLevels.SelectedValue = puzzle.DifficultyLevelId;
                        fill.listBox.SelectedItem = puzzle;

                        MessageBoxes.Info("Успешно.");
                    }
                    catch (MySqlException ex) 
                    when (ex.Number == 1062)
                    {
                        MessageBoxes.Error("Название пазла занято.");
                    }
                    catch (Exception ex)
                    {
                        MessageBoxes.Error(ex.Message);
                    }
                });

                form.ShowDialog();
            });
            #endregion
        }
       
        internal void DisplayGameChoice()
        {
            Size = normalFormSize;
            CenterToScreen();

            #region Right
            var right = new ImageAndMethodsUC()
            {
                Dock = DockStyle.Right
            };
            ChangeRight(right);
            right.panelMethods.Visible = true;

            right.ButtonRecords1Click = new EventHandler((s, e) => ShowRecordsForm(1));
            right.ButtonRecords2Click = new EventHandler((s, e) => ShowRecordsForm(2));

            switch (currMethodId)
            {
                case 1:
                    right.radioButton1.Select();
                    break;
                case 2:
                    right.radioButton2.Select();
                    break;
                case 0:
                    right.radioButton0.Select();
                    break;
            }
            #endregion
            #region Fill
            var fill = new listWithPictogramsUC()
            {
                Dock = DockStyle.Fill,
            };
            ChangeFill(fill);
            fill.panelLevel.Visible = true;

            fill.comboBoxLevels.SelectedValueChanged += new EventHandler((s, e) =>
            {
                bindingSourceFilteredLevels.Position = fill.comboBoxLevels.SelectedIndex;
                FilterPuzzles(fill.dataGridView);
            });

            fill.dataGridView.SelectionChanged += new EventHandler((s, e) =>
            {
                bindingSourceFilteredPuzzles.Position = fill.dataGridView.CurrentRow.Index;
                DisplayImage(right);
                DisplaySavedGames(right);
            });

            FilterLevels(fill.comboBoxLevels);
            #endregion            
            #region Top
            topControl.buttonBack.Visible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                DisposeImage(right);
                DisplayRegAndAuth();
            });
            topControl.buttonImage.Visible = false;
            topControl.buttonSound.Visible = true;
            topControl.buttonSound.ImageIndex = soundOn ? 2 : 3;
            topControl.ButtonSoundClick = new EventHandler((s, e) =>
            {
                soundOn = !soundOn;
                topControl.buttonSound.ImageIndex = soundOn ? 2 : 3;
            });
            topControl.labelTitle.Text = "Выбор игры";
            topControl.labelMethod.Visible = false;
            topControl.labelValue.Visible = false;
            #endregion            
            #region Bottom
            panelBottom.Show();
            bottomControl.ButtonDeleteVisible = false;
            bottomControl.ButtonUpdateVisible = false;

            right.radioButton1.CheckedChanged += new EventHandler((s, e) =>
            {
                bottomControl.ButtonLoadEnabled = savedMethods.Contains(1);
            });
            right.radioButton2.CheckedChanged += new EventHandler((s, e) =>
            {
                bottomControl.ButtonLoadEnabled = savedMethods.Contains(2);
            });
            right.radioButton0.CheckedChanged += new EventHandler((s, e) =>
            {
                bottomControl.ButtonLoadEnabled = savedMethods.Contains(0);
            });
            bottomControl.ButtonLoadEnabled = savedMethods.Contains(currMethodId);
            bottomControl.ButtonLoadVisible = true;
            bottomControl.ButtonLoadClick = new EventHandler((s, e) =>
            {
                var puzzle = (PuzzleVM)bindingSourceFilteredPuzzles.Current;

                InitTopControl(right, puzzle);

                DisplayGame(puzzle, CreateSavedGame(puzzle));
            });

            bottomControl.ButtonInsertOrNewGameText = "Новая игра";
            bottomControl.ButtonInsertOrNewGameClick = new EventHandler((s, e) =>
            {
                var puzzle = (PuzzleVM)bindingSourceFilteredPuzzles.Current;

                InitTopControl(right, puzzle);

                bool hasSavedGame = savedGames
                    .Where(i => i.PuzzleId == puzzle.Id
                        && i.CountingMethodId == puzzle.CountingMethodId)
                    .Any();
                if (!hasSavedGame)
                {
                    DisplayGame(puzzle, CreateNewGame(puzzle));
                }
                else 
                {
                    var result = MessageBoxes.Question2("Удалить сохраненную игру и начать заного?");
                    if (result == DialogResult.OK)
                    {
                        try
                        {
                            var savedGame = savedGames
                                .Where(i => i.PuzzleId == puzzle.Id
                                    && i.CountingMethodId == puzzle.CountingMethodId)
                                .Single();

                            var p1 = new MySqlParameter("@p1", savedGame.SavedGameId);
                            using (var db = new PuzzleContext(Db.Options))
                            {
                                int rowsAffected = db.Database.ExecuteSqlRaw("CALL `delete_game` (@p1)", p1);
                                if (rowsAffected != 1)
                                {
                                    throw new Exception("Ошибка.");
                                }
                            }

                            savedGames = Db.LoadGames();

                            MessageBoxes.Info("Успешно.");
                        }
                        catch (Exception ex)
                        {
                            MessageBoxes.Error(ex.Message);
                        }

                        DisplayGame(puzzle, CreateNewGame(puzzle));
                    }
                }
            });
            #endregion
        }
        internal void DisplayGame(PuzzleVM puzzle, Game game)
        {
            Size = normalFormSize;
            CenterToScreen();

            ChangeRight(null);
            #region Fill
            var fill = new GameUC(puzzle, game, this)
            {
                Dock = DockStyle.Fill,
            };
            ChangeFill(fill);
            #endregion            
            #region Top
            Image tempFieldImage = fill.pictureBoxField.Image;

            topControl.buttonBack.Visible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                if (game.CountingMethodId == 2)
                {
                    topControl.timer.Stop();
                    fill.pictureBoxField.Image = game.FullImage;
                }

                var result = MessageBoxes.Question3("Сохранить игру?");
                if (result == DialogResult.Cancel)
                {
                    if (game.CountingMethodId == 2)
                    {
                        fill.pictureBoxField.Image = tempFieldImage;
                        topControl.timer.Start();
                    }
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    try
                    {
                        var p1 = new MySqlParameter("@p1", ResultDTO.PlayerId);
                        var p2 = new MySqlParameter("@p2", puzzle.Id);
                        var p3 = new MySqlParameter("@p3", game.FragmentNumbers);
                        var p4 = new MySqlParameter("@p4", game.CountingMethodId);
                        var p5 = new MySqlParameter("@p5", game.Score);
                        var p6 = new MySqlParameter("@p6", game.Time);
                        using (var db = new PuzzleContext(Db.Options))
                        {
                            int rowsAffected = db.Database.ExecuteSqlRaw("CALL `save_game` (@p1, @p2, @p3, @p4, @p5, @p6)", p1, p2, p3, p4, p5, p6);
                            if (rowsAffected != 1)
                            {
                                throw new Exception("Ошибка.");
                            }
                        }

                        savedGames = Db.LoadGames();

                        MessageBoxes.Info("Успешно.");
                    }
                    catch (Exception ex)
                    {
                        MessageBoxes.Error(ex.Message);
                    }
                }

                DisplayGameChoice();
            });

            topControl.buttonImage.Visible = true;
            bool imageOn = false;
            topControl.buttonImage.ImageIndex = imageOn ? 7 : 6;            
            topControl.ButtonImageClick = new EventHandler((s, e) =>
            {
                imageOn = !imageOn;
                topControl.buttonImage.ImageIndex = imageOn ? 7 : 6;
                                
                if (imageOn)
                {
                    fill.pictureBoxField.Image = game.FullImage;
                }
                else
                {
                    fill.pictureBoxField.Image = tempFieldImage;
                }

                if (game.CountingMethodId == 2)                
                {
                    if (imageOn)
                    {
                        topControl.timer.Stop();
                    }
                    else
                    {
                        topControl.timer.Start();
                    }
                }
            });

            topControl.labelTitle.Text = puzzle.Name;
            #endregion            
            panelBottom.Hide();
        }
    }
}
