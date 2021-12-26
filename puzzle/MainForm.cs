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
            bindingSourcePuzzles.DataSource = new BindingList<PuzzleVM>(Db.LoadPuzzles());
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

        public void DisplayRegAndAuth()
        {
            Size = smallFormSize;
            CenterToScreen();

            var fill = new RegAndAuthUC(this)
            {
                Dock = DockStyle.Fill
            };
            ChangeFill(fill);
            ChangeRight(null);

            topControl.ButtonBackVisible = false;
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;
            topControl.LabelTitleText = "Регистрация/авторизация";

            panelBottom.Hide();
        }
        public void DisplayAdminMenu()
        {
            Size = smallFormSize;
            CenterToScreen();

            var fill = new AdminMenuUC(this)
            {
                Dock = DockStyle.Fill
            };
            ChangeFill(fill);
            ChangeRight(null);

            topControl.ButtonBackVisible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                DisplayRegAndAuth();
            });
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;
            topControl.LabelTitleText = "Меню администратора";

            panelBottom.Hide();
        }
        public void DisplayGallery()
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

                if (bindingSourceGallery.List.Count > 0)
                {
                    selectedItem = (ImageVM)bindingSourceGallery.List[fill.listBox.SelectedIndex];
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
            topControl.ButtonBackVisible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                DisposeImage(right);
                DisplayAdminMenu();
            });
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;
            topControl.LabelTitleText = "Галерея";
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
        public void DisplayLevels()
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

            bindingSourceLevels.ListChanged += new ListChangedEventHandler((s, e) =>
            {
                var selectedItem = (LevelVM)fill.listBox.SelectedItem;
                if (selectedItem == null)
                {
                    return;
                }

                if (bindingSourceLevels.List.Count > 0)
                {
                    selectedItem = (LevelVM)bindingSourceLevels.List[fill.listBox.SelectedIndex];

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
            topControl.ButtonBackVisible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                DisplayAdminMenu();
            });
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;
            topControl.LabelTitleText = "Уровни сложности";
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
        public void DisplayPuzzles()
        {
            Size = normalFormSize;
            CenterToScreen();

            #region Right
            var right = new ImageAndMethodsUC()
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

            fill.panelLevel.Visible = true;
            
            fill.comboBoxLevel.SelectedValueChanged += new EventHandler((s, e) =>
            {
                var selectedItem = (LevelVM)fill.comboBoxLevel.SelectedItem;
                if (selectedItem == null)
                {
                    return;
                }
                bindingSourcePuzzles.Filter = $"difficultyLevelId = {selectedItem.Id}";
            });
            fill.comboBoxLevel.DataSource = bindingSourceLevels.DataSource;
            fill.comboBoxLevel.DisplayMember = "Name";
            /*
            bindingSourcePuzzles.ListChanged += new ListChangedEventHandler((s, e) =>
            {
                var selectedItem = (PuzzleVM)fill.listBox.SelectedItem;
                if (selectedItem == null)
                {
                    return;
                }

                if (bindingSourcePuzzles.List.Count > 0)
                {
                    selectedItem = (PuzzleVM)bindingSourcePuzzles.List[fill.listBox.SelectedIndex];

                    right.pictureBoxImage.Image = Image.FromStream(
                        ((IList<ImageVM>)bindingSourceGallery.List)
                        .Where(i => i.Id == selectedItem.ImageId)
                        .Select(i => i.Image).Single());                
                }
            });

            fill.listBox.SelectedValueChanged += new EventHandler((s, e) =>
            {
                var selectedItem = (PuzzleVM)fill.listBox.SelectedItem;
                if (selectedItem == null)
                {
                    return;
                }

                right.pictureBoxImage.Image = Image.FromStream(
                    ((IList<ImageVM>)bindingSourceGallery.List)
                    .Where(i => i.Id == selectedItem.ImageId)
                    .Select(i => i.Image).Single());
            });*/
            fill.listBox.DataSource = bindingSourcePuzzles.DataSource;
            #endregion
            #region Top
            topControl.ButtonBackVisible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                DisposeImage(right);
                DisplayAdminMenu();
            });
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;
            topControl.LabelTitleText = "Пазлы";
            #endregion
            #region Bottom
            panelBottom.Show();
            bottomControl.ButtonDeleteVisible = true;
            bottomControl.ButtonDeleteClick = new EventHandler((s, e) =>
            {
                if (fill.listBox.SelectedItems.Count == 0) return;
                try
                {
                    using (var db = new PuzzleContext(Db.Options))
                    {
                        //Db.Instance.Puzzles.Remove((Puzzle)fill.listBox.SelectedItem);
                        //Db.Instance.SaveChanges();
                    }
                    fill.listBox.ClearSelected();
                    right.Visible = false;
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
                if (fill.listBox.SelectedItems.Count == 0) return;
                var form = new InsertOrUpdatePuzzleForm(this)
                {
                    Text = "Изменение пазла",
                };
                form.textBoxName.Text = ((Puzzle)fill.listBox.SelectedItem).Name;
                form.comboBoxImage.SelectedItem = ((Puzzle)fill.listBox.SelectedItem).Image;
                form.comboBoxLevel.SelectedItem = ((Puzzle)fill.listBox.SelectedItem).DifficultyLevel;
                var gallery = (Gallery)form.comboBoxImage.SelectedItem;
                var image = LocalStorage.LoadImage(gallery.Path);
                MyPuzzle myPuzzle = new(
                    ((Puzzle)fill.listBox.SelectedItem).DifficultyLevel.FragmentTypeId,
                    ((Puzzle)fill.listBox.SelectedItem).DifficultyLevel.AssemblyTypeId,
                    ((Puzzle)fill.listBox.SelectedItem).DifficultyLevel.HorizontalFragmentCount,
                    ((Puzzle)fill.listBox.SelectedItem).DifficultyLevel.VerticalFragmentCount,
                    image);
                MyPuzzle.Instance.SplitIntoFragments();
                if (((Puzzle)fill.listBox.SelectedItem).DifficultyLevel.AssemblyTypeId == 1)
                {
                    //Db.Instance.PuzzleFields.Load();
                    //MyPuzzle.Instance.FragmentNumbers = Db.Instance.PuzzleFields.Find(((Puzzle)fill.listBox.SelectedItem).PuzzleId).FragmentNumbers;
                }
                else
                {
                    //Db.Instance.PuzzleTapes.Load();
                    //MyPuzzle.Instance.FragmentNumbers = Db.Instance.PuzzleTapes.Find(((Puzzle)fill.listBox.SelectedItem).PuzzleId).FragmentNumbers;
                }
                using var graphics = form.pictureBoxField.CreateGraphics();
                MyPuzzle.Instance.DrawField(graphics);
                form.buttonInsertOrUpdate.Text = "Изменить";
                form.ButtonInsertOrUpdateClick = new EventHandler((s, e) =>
                {
                    try
                    {
                        //SetPuzzleDTO(form);

                        var id = new MySqlConnector.MySqlParameter("@id", ((Puzzle)fill.listBox.SelectedItem).PuzzleId);
                        var p1 = new MySqlConnector.MySqlParameter("@p1", PuzzleDTO.Name);
                        var p2 = new MySqlConnector.MySqlParameter("@p2", PuzzleDTO.ImageId);
                        var p3 = new MySqlConnector.MySqlParameter("@p3", PuzzleDTO.DifficultyLevelId);
                        var p4 = new MySqlConnector.MySqlParameter("@p4", PuzzleDTO.FragmentNumbers);
                        using (var db = new PuzzleContext(Db.Options))
                        {
                            int rowsAffected = db.Database.ExecuteSqlRaw("CALL `update_puzzle` (@id, @p1, @p2, @p3, @p4)", id, p1, p2, p3, p4);
                            if (rowsAffected != 1)
                            {
                                throw new Exception("Ошибка.");
                            }
                        }/*
                        Db.Instance.Puzzles.Update((Puzzle)fill.listBox.SelectedItem);
                        Db.Instance.SaveChanges();*/
                        MessageBoxes.Info("Успешно.");
                    }
                    catch (MySqlConnector.MySqlException ex) when (ex.Number == 1062)
                    {
                        if (ex.Message.Contains("'puzzles.name'"))
                        {
                            MessageBoxes.Error("Название пазла занято.");
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
                form.ShowDialog();
                /*
                Db.Instance.Dispose();
                Db.Instance = new PuzzleContext(Db.Options);
                Db.FirstLoad();
                Db.LoadGalleries();
                Db.LoadDifficultyLevels();
                Db.LoadPuzzles(); 
                fill.comboBoxLevel.DataSource = Db.Instance.DifficultyLevels.Local.ToBindingList();
                fill.comboBoxLevel.DisplayMember = "Name";
                fill.comboBoxLevel.ValueMember = "DifficultyLevelId";
                fill.listBox.DataSource = Db.Instance.Puzzles.Local.ToBindingList();
                fill.listBox.DisplayMember = "Name";
                fill.listBox.ValueMember = "PuzzleId";*/
                fill.listBox.ClearSelected();
                right.Visible = false;
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

                        var puzzle = new PuzzleVM
                        {
                            Name = form.textBoxName.Text,
                            ImageId = ((Gallery)form.comboBoxImage.SelectedItem).ImageId,
                            DifficultyLevelId = ((DifficultyLevel)form.comboBoxLevel.SelectedItem).DifficultyLevelId,
                            //FragmentNumbers = MyPuzzle.Instance.FragmentNumbers
                        };

                        var p1 = new MySqlConnector.MySqlParameter("@p1", PuzzleDTO.Name);
                        var p2 = new MySqlConnector.MySqlParameter("@p2", PuzzleDTO.ImageId);
                        var p3 = new MySqlConnector.MySqlParameter("@p3", PuzzleDTO.DifficultyLevelId);
                        var p4 = new MySqlConnector.MySqlParameter("@p4", PuzzleDTO.FragmentNumbers);
                        using (var db = new PuzzleContext(Db.Options))
                        {
                            int rowsAffected = db.Database.ExecuteSqlRaw("CALL `insert_puzzle` (@p1, @p2, @p3, @p4)", p1, p2, p3, p4);
                            if (rowsAffected != 1)
                            {
                                throw new Exception("Ошибка.");
                            }
                        }
                        Db.LoadPuzzles();
                        MessageBoxes.Info("Успешно.");
                    }
                    catch (MySqlConnector.MySqlException ex) when (ex.Number == 1062)
                    {
                        if (ex.Message.Contains("'puzzles.name'"))
                        {
                            MessageBoxes.Error("Название пазла занято.");
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
                form.ShowDialog();
                //Db.LoadDifficultyLevels();
                fill.listBox.ClearSelected();
                right.Visible = false;
            });
            #endregion
        }
    }
}
