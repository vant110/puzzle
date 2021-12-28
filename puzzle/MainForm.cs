using Microsoft.EntityFrameworkCore;
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

namespace puzzle
{
    public partial class MainForm : Form
    {
        private readonly Size smallFormSize = new(313, 243);
        private readonly Size normalFormSize = new(800, 600);

        private TopUC topControl;
        private BottomUC bottomControl;
        private UserControl fillControl;
        private UserControl rightControl;

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

            bindingSourceGallery.DataSource = new BindingList<ImageVM>(Db.LoadGallery());
            bindingSourceLevels.DataSource = new BindingList<LevelVM>(Db.LoadLevels());
            bindingSourcePuzzles.DataSource = new BindingList<PuzzleVM>(Db.LoadPuzzles((IList<ImageVM>)bindingSourceGallery.List));
            using (var db = new PuzzleContext(Db.Options)) {
                db.FragmentTypes.Load();
                db.AssemblyTypes.Load();
                db.CountingMethods.Load();
                bindingSourceFragmentTypes.DataSource = db.FragmentTypes.Local.ToBindingList();
                bindingSourceAssemblyTypes.DataSource = db.AssemblyTypes.Local.ToBindingList();
                bindingSourceMethods.DataSource = db.CountingMethods.Local.ToBindingList();
            }            
        }

        private static void DisposeImage(ImageAndMethodsUC right)
        {
            if (right.pictureBoxImage.Image == null) return;
            right.pictureBoxImage.Image.Dispose();
            right.pictureBoxImage.Image = null;
        }

        private void ChangeFill(UserControl newFillControl)
        {
            panelCenter.Controls.Remove(fillControl);
            if (fillControl != null)
            {
                fillControl.Dispose();
            }
            fillControl = newFillControl;
            panelCenter.Controls.Add(fillControl);
            fillControl.BringToFront();
        }
        private void ChangeRight(UserControl newRightControl)
        {
            panelCenter.Controls.Remove(rightControl);
            if (rightControl != null)
            {
                rightControl.Dispose();
            }
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
        private void FilterPuzzles(Control listOrGrid)
        {
            Debug.WriteLine(1);

            if (bindingSourceFilteredLevels.Current == null)
            {
                bindingSourceFilteredPuzzles.Clear();
                return;
            }

            var filteredPuzzles = ((IList<PuzzleVM>)bindingSourcePuzzles.List)
                .Where(i => i.DifficultyLevelId == ((LevelVM)bindingSourceFilteredLevels.Current).Id)
                .OrderBy(i => i.Name)
                .ToList();
            bindingSourceFilteredPuzzles.DataSource = new BindingList<PuzzleVM>(filteredPuzzles);

            if (listOrGrid is ListBox listBox)
            {
                listBox.DataSource = bindingSourceFilteredPuzzles.DataSource;
            }
            else if (listOrGrid is DataGridView dataGridView)
            {
                dataGridView.AutoGenerateColumns = false;
                dataGridView.DataSource = bindingSourceFilteredPuzzles.DataSource;
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    // !!! если нет сохраненной игры
                    dataGridView.Rows[i].Cells[1].Value = new Bitmap(1, 1);
                }
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

        private Game CreateGame(PuzzleVM puzzle)
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

            return game;
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
            topControl.buttonPause.Visible = false;
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
            topControl.buttonPause.Visible = false;
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
                if (selectedItem == null)
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
                if (selectedItem == null)
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
            topControl.buttonPause.Visible = false;
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
                if (selectedItem == null) return;
                try
                {
                    var id = new MySqlConnector.MySqlParameter("@id", selectedItem.Id);
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
                catch (MySqlConnector.MySqlException ex)
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
                        if (newImage.Image == null)
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

                        var p1 = new MySqlConnector.MySqlParameter("@p1", image.Name);
                        var p2 = new MySqlConnector.MySqlParameter("@p2", image.Path);
                        var p3 = new MySqlConnector.MySqlParameter("@p3", image.ImageHash);
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
                    catch (MySqlConnector.MySqlException ex) 
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
                if (selectedItem == null)
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
                if (selectedItem == null)
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
            topControl.buttonPause.Visible = false;
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
                if (selectedItem == null) return;
                try
                {
                    var id = new MySqlConnector.MySqlParameter("@id", selectedItem.Id);
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
                catch (MySqlConnector.MySqlException ex)
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
                if (selectedItem == null) return;
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
                        if (form.comboBoxFragmentType.SelectedItem == null)
                        {
                            throw new Exception("Выберите тип фрагментов.");
                        }
                        if (form.comboBoxAssemblyType.SelectedItem == null)
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

                        var id = new MySqlConnector.MySqlParameter("@id", level.Id);
                        var p1 = new MySqlConnector.MySqlParameter("@p1", level.Name);
                        var p2 = new MySqlConnector.MySqlParameter("@p2", level.HorizontalFragmentCount);
                        var p3 = new MySqlConnector.MySqlParameter("@p3", level.VerticalFragmentCount);
                        var p4 = new MySqlConnector.MySqlParameter("@p4", level.FragmentTypeId);
                        var p5 = new MySqlConnector.MySqlParameter("@p5", level.AssemblyTypeId);
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
                    catch (MySqlConnector.MySqlException ex)
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
                        if (form.comboBoxFragmentType.SelectedItem == null)
                        {
                            throw new Exception("Выберите тип фрагментов.");
                        }
                        if (form.comboBoxAssemblyType.SelectedItem == null)
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

                        var p1 = new MySqlConnector.MySqlParameter("@p1", level.Name);
                        var p2 = new MySqlConnector.MySqlParameter("@p2", level.HorizontalFragmentCount);
                        var p3 = new MySqlConnector.MySqlParameter("@p3", level.VerticalFragmentCount);
                        var p4 = new MySqlConnector.MySqlParameter("@p4", level.FragmentTypeId);
                        var p5 = new MySqlConnector.MySqlParameter("@p5", level.AssemblyTypeId);
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
                    catch (MySqlConnector.MySqlException ex)
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
            topControl.buttonPause.Visible = false;
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
                if (puzzle == null) return;
                try
                {
                    var id = new MySqlConnector.MySqlParameter("@id", puzzle.Id);
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
                if (puzzle == null) return;
                var form = new InsertOrUpdatePuzzleForm(this)
                {
                    Text = "Изменение пазла",
                };

                form.textBoxName.Text = puzzle.Name;
                form.comboBoxImage.SelectedValue = puzzle.ImageId;
                form.comboBoxLevel.SelectedValue = puzzle.DifficultyLevelId;

                Game.Instance = CreateGame(puzzle);

                form.buttonInsertOrUpdate.Text = "Изменить";
                form.ButtonInsertOrUpdateClick = new EventHandler((s, e) =>
                {
                    try
                    {
                        if (!Validator.IsPuzzleName(form.textBoxName.Text))
                        {
                            throw new Exception("Название пазла некорректно.");
                        }
                        if (form.comboBoxImage.SelectedItem == null)
                        {
                            throw new Exception("Выберите изображение.");
                        }
                        if (form.comboBoxLevel.SelectedItem == null)
                        {
                            throw new Exception("Выберите уровень сложности.");
                        }
                        if (Game.Instance == null)
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

                        var id = new MySqlConnector.MySqlParameter("@id", puzzle.Id);
                        var p1 = new MySqlConnector.MySqlParameter("@p1", tempPuzzle.Name);
                        var p2 = new MySqlConnector.MySqlParameter("@p2", tempPuzzle.ImageId);
                        var p3 = new MySqlConnector.MySqlParameter("@p3", tempPuzzle.DifficultyLevelId);
                        var p4 = new MySqlConnector.MySqlParameter("@p4", tempPuzzle.FragmentNumbers);
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
                    catch (MySqlConnector.MySqlException ex) 
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
                        if (form.comboBoxImage.SelectedItem == null)
                        {
                            throw new Exception("Выберите изображение.");
                        }
                        if (form.comboBoxLevel.SelectedItem == null)
                        {
                            throw new Exception("Выберите уровень сложности.");
                        }
                        if (Game.Instance == null)
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

                        var p1 = new MySqlConnector.MySqlParameter("@p1", puzzle.Name);
                        var p2 = new MySqlConnector.MySqlParameter("@p2", puzzle.ImageId);
                        var p3 = new MySqlConnector.MySqlParameter("@p3", puzzle.DifficultyLevelId);
                        var p4 = new MySqlConnector.MySqlParameter("@p4", puzzle.FragmentNumbers);
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
                    catch (MySqlConnector.MySqlException ex) 
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
            topControl.buttonPause.Visible = false;
            topControl.buttonImage.Visible = false;
            topControl.buttonSound.Visible = true;
            topControl.labelTitle.Text = "Выбор игры";
            topControl.labelMethod.Visible = false;
            topControl.labelValue.Visible = false;
            #endregion            
            #region Bottom
            panelBottom.Show();
            bottomControl.ButtonDeleteVisible = false;
            bottomControl.ButtonUpdateVisible = false;
            bottomControl.ButtonLoadVisible = true;
            bottomControl.ButtonInsertOrNewGameClick = new EventHandler((s, e) =>
            {                
                DisplayGame((PuzzleVM)bindingSourceFilteredPuzzles.Current);
            });

            bottomControl.ButtonInsertOrNewGameText = "Новая игра";
            bottomControl.ButtonInsertOrNewGameClick = new EventHandler((s, e) =>
            {
                DisplayGame((PuzzleVM)bindingSourceFilteredPuzzles.Current);
            });
            #endregion
        }
        internal void DisplayGame(PuzzleVM puzzle)
        {
            Size = normalFormSize;
            CenterToScreen();

            ChangeRight(null);
            #region Fill
            var fill = new GameUC(CreateGame(puzzle), this)
            {
                Dock = DockStyle.Fill,
            };
            ChangeFill(fill);
            #endregion            
            #region Top
            topControl.buttonBack.Visible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                DisplayGameChoice();
            });

            topControl.buttonPause.Visible = false;
            topControl.buttonPause.Click += new EventHandler((s, e) =>
            {
            });

            topControl.buttonImage.Visible = true;
            topControl.buttonImage.Click += new EventHandler((s, e) =>
            {
            });

            topControl.buttonSound.Visible = true;
            topControl.buttonSound.Click += new EventHandler((s, e) =>
            {
            });

            topControl.labelTitle.Text = puzzle.Name;
            topControl.labelMethod.Visible = false;
            topControl.labelValue.Visible = false;
            #endregion            
            panelBottom.Hide();
        }
    }
}
