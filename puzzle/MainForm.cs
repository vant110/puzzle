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

namespace puzzle
{
    public partial class MainForm : Form
    {
        private readonly Size smallSize = new(313, 243);
        private readonly Size normalSize = new(800, 600);

        private readonly TopControl topControl;
        private readonly BottomControl bottomControl;
        private Control fillControl;
        private Control rightControl;

        public MainForm()
        {
            InitializeComponent();

            #region Top
            topControl = new TopControl
            {
                Dock = DockStyle.Top
            };
            panelTop.Controls.Add(topControl);
            #endregion
            #region Bottom
            bottomControl = new BottomControl
            {
                Dock = DockStyle.Bottom
            };
            panelBottom.Controls.Add(bottomControl);
            #endregion
            ConfigureOnRegAndAuth();

            #region Инициализация options
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<PuzzleContext>();
            Db.Options = optionsBuilder
                .UseMySql(connectionString, ServerVersion.Parse("8.0.27-mysql"))
                .Options;
            #endregion
            optionsBuilder.LogTo(message => Debug.WriteLine(message));

            Db.Instance = new PuzzleContext(Db.Options);
            Db.FirstLoad();
        }

        private void ChangeFill(Control newFillControl)
        {
            panelCenter.Controls.Remove(fillControl);
            fillControl = newFillControl;
            panelCenter.Controls.Add(fillControl);
            fillControl.BringToFront();
        }

        private void ChangeRight(Control newRightControl)
        {
            panelCenter.Controls.Remove(rightControl);
            rightControl = newRightControl;
            panelCenter.Controls.Add(rightControl);
        }

        private void DisposeImage(ImageAndMethodsControl right)
        {
            if (right.pictureBoxImage.Image == null) return;
            right.pictureBoxImage.Image.Dispose();
            right.pictureBoxImage.Image = null;
        }

        private void SetPuzzleDTO(InsertOrUpdatePuzzleForm form)
        {
            PuzzleDTO.Name = form.textBoxName.Text;
            if (!Validator.IsPuzzleName(PuzzleDTO.Name))
            {
                throw new Exception("Название пазла некорректно.");
            }
            if (form.comboBoxImage.SelectedItem == null)
            {
                throw new Exception("Выберите изображение.");
            }
            PuzzleDTO.ImageId = ((Gallery)form.comboBoxImage.SelectedItem).ImageId;
            if (form.comboBoxLevel.SelectedItem == null)
            {
                throw new Exception("Выберите уровень сложности.");
            }
            PuzzleDTO.DifficultyLevelId = ((DifficultyLevel)form.comboBoxLevel.SelectedItem).DifficultyLevelId;
            PuzzleDTO.FragmentNumbers = MyPuzzle.Instance.FragmentNumbers;             
        }

        public void ConfigureOnRegAndAuth()
        {
            var fill = new RegAndAuthControl(this)
            {
                Dock = DockStyle.Fill
            };
            Size = smallSize;
            CenterToScreen();
            ChangeFill(fill);
            ChangeRight(null);

            topControl.ButtonBackVisible = false;
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;
            topControl.LabelTitleText = "Регистрация/авторизация";

            panelBottom.Hide();
        }

        public void ConfigureOnAdminMenu()
        {
            var fill = new AdminMenuControl(this)
            {
                Dock = DockStyle.Fill
            };
            Size = smallSize;
            CenterToScreen();
            ChangeFill(fill);
            ChangeRight(null);

            topControl.ButtonBackVisible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                ConfigureOnRegAndAuth();
            });
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;
            topControl.LabelTitleText = "Меню администратора";

            panelBottom.Hide();
        }

        public void ConfigureOnGallery()
        {
            Db.LoadGalleries();

            Size = normalSize;
            CenterToScreen();
            #region Right
            var right = new ImageAndMethodsControl()
            {
                Dock = DockStyle.Right,
                PanelMethodsVisible = false
            };
            ChangeRight(right);
            #endregion
            #region Fill
            var fill = new UserControls.ListControl()
            {
                Dock = DockStyle.Fill,
            };
            ChangeFill(fill);

            fill.PanelLevelVisible = false;
            fill.listBox.DataSource = Db.Instance.Galleries.Local.ToBindingList();
            fill.listBox.DisplayMember = "Name";
            fill.listBox.ValueMember = "ImageId";
            fill.listBox.ClearSelected();
            right.Visible = false;
            fill.listBox.SelectedValueChanged += new EventHandler((s, e) =>
            {
                DisposeImage(right);
                if (fill.listBox.SelectedItems.Count == 0) return;
                right.Visible = true;
                string path = ((Gallery)fill.listBox.SelectedItem).Path;
                right.pictureBoxImage.Image = LocalStorage.LoadImage(path);
            });
            #endregion
            #region Top
            topControl.ButtonBackVisible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                ConfigureOnAdminMenu();
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
                if (fill.listBox.SelectedItems.Count == 0) return;
                try
                {
                    var p1 = new MySqlConnector.MySqlParameter("@id", fill.listBox.SelectedValue);
                    int rowsAffected = Db.Instance.Database.ExecuteSqlRaw("CALL `delete_image` (@id)", p1);
                    if (rowsAffected != 1)
                    {
                        throw new Exception("Ошибка.");
                    }
                    DisposeImage(right);
                    LocalStorage.Delete(((Gallery)fill.listBox.SelectedItem).Path);
                    Db.LoadGalleries();
                    fill.listBox.ClearSelected();
                    right.Visible = false;
                    DisposeImage(right);
                    MessageBoxes.Info("Успешно.");
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
                new InsertImageForm().ShowDialog();
                Db.LoadGalleries();
                fill.listBox.ClearSelected();
                right.Visible = false;
                DisposeImage(right);
            });
            #endregion
        }

        public void ConfigureOnLevels()
        {
            Db.LoadDifficultyLevels();

            Size = normalSize;
            CenterToScreen();
            #region Right
            var right = new LevelInfoControl()
            {
                Dock = DockStyle.Right
            };
            ChangeRight(right);
            #endregion
            #region Fill
            var fill = new UserControls.ListControl()
            {
                Dock = DockStyle.Fill,
            };
            ChangeFill(fill);
            fill.PanelLevelVisible = false;

            BindingList<LevelViewModel> levels = new BindingList<LevelViewModel>();
            foreach (var level in Db.GetLevels())
            {
                levels.Add(level);
            }
            bindingSourceLevels.DataSource = levels;
            fill.listBox.DataSource = bindingSourceLevels.DataSource; 
            fill.listBox.DisplayMember = "Name";
            fill.listBox.ValueMember = "Id";
            fill.listBox.SelectedValueChanged += new EventHandler((s, e) =>
            {
                if (fill.listBox.SelectedItem == null) return;
                right.labelHorizontal.Text = ((LevelViewModel)fill.listBox.SelectedItem).HorizontalFragmentCount.ToString();
                right.labelVertical.Text = ((LevelViewModel)fill.listBox.SelectedItem).VerticalFragmentCount.ToString();
                right.labelFragmentType.Text = ((LevelViewModel)fill.listBox.SelectedItem).FragmentTypeId.ToString(); // !!!
                right.labelAssemblyType.Text = ((LevelViewModel)fill.listBox.SelectedItem).AssemblyTypeId.ToString(); // !!!
            });
            #endregion
            #region Top
            topControl.ButtonBackVisible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                ConfigureOnAdminMenu();
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
                if (fill.listBox.SelectedItems.Count == 0) return;
                try
                {
                    Db.Instance.DifficultyLevels.Remove((DifficultyLevel)fill.listBox.SelectedItem);
                    Db.Instance.SaveChanges();
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
                if (fill.listBox.SelectedItem == null) return;
                var form = new InsertOrUpdateLevelForm
                {
                    Text = "Изменение уровня сложности",
                };
                form.textBoxName.Text = 
                    ((LevelViewModel)fill.listBox.SelectedItem).Name;
                form.numericUpDownHorizontal.Value = 
                    ((LevelViewModel)fill.listBox.SelectedItem).HorizontalFragmentCount;
                form.labelVertical.Text = 
                    ((LevelViewModel)fill.listBox.SelectedItem).VerticalFragmentCount.ToString();
                form.comboBoxFragmentType.SelectedValue = 
                    ((LevelViewModel)fill.listBox.SelectedItem).FragmentTypeId;
                form.comboBoxAssemblyType.SelectedValue = 
                    ((LevelViewModel)fill.listBox.SelectedItem).AssemblyTypeId;
                form.buttonInsertOrUpdate.Text = "Изменить";
                form.ButtonInsertOrUpdateClick = new EventHandler((s, e) =>
                {
                    try
                    {
                        if (form.comboBoxFragmentType.SelectedItem == null)
                        {
                            throw new Exception("Выберите тип фрагментов.");
                        }
                        if (form.comboBoxAssemblyType.SelectedItem == null)
                        {
                            throw new Exception("Выберите тип сборки.");
                        }
                        var level = ((LevelViewModel)fill.listBox.SelectedItem);
                        level.Name = form.textBoxName.Text;
                        level.HorizontalFragmentCount = (sbyte)form.numericUpDownHorizontal.Value;
                        level.FragmentTypeId = ((FragmentType)form.comboBoxFragmentType.SelectedItem).FragmentTypeId;
                        level.AssemblyTypeId = ((AssemblyType)form.comboBoxAssemblyType.SelectedItem).AssemblyTypeId;

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
                                throw new Exception("Можно изменить только название уровня сложности (пока он используется хотя бы в одном пазле).");
                            }
                        }               
                        MessageBoxes.Info("Успешно.");
                    }
                    catch (MySqlConnector.MySqlException ex) when (ex.Number == 1062)
                    {
                        if (ex.Message.Contains("'difficulty_levels.name'"))
                        {
                            //throw;// !!!
                            MessageBoxes.Error("Название уровня сложности занято.");
                        }
                        throw;// ???
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
                var form = new InsertOrUpdateLevelForm
                {
                    Text = "Добавление уровня сложности",
                };
                form.buttonInsertOrUpdate.Text = "Добавить";
                form.ButtonInsertOrUpdateClick = new EventHandler((s, e) =>
                {
                    try
                    {
                        //SetLevelDTO(form);

                        var p1 = new MySqlConnector.MySqlParameter("@p1", LevelDTO.Name);
                        var p2 = new MySqlConnector.MySqlParameter("@p2", LevelDTO.HorizontalFragmentCount);
                        var p3 = new MySqlConnector.MySqlParameter("@p3", LevelDTO.VerticalFragmentCount);
                        var p4 = new MySqlConnector.MySqlParameter("@p4", LevelDTO.FragmentTypeId);
                        var p5 = new MySqlConnector.MySqlParameter("@p5", LevelDTO.AssemblyTypeId);
                        int rowsAffected = Db.Instance.Database.ExecuteSqlRaw("CALL `insert_level` (@p1, @p2, @p3, @p4, @p5)", p1, p2, p3, p4, p5);
                        if (rowsAffected != 1)
                        {
                            throw new Exception("Ошибка.");
                        }
                        MessageBoxes.Info("Успешно.");
                    }
                    catch (MySqlConnector.MySqlException ex) when (ex.Number == 1062)
                    {
                        if (ex.Message.Contains("'difficulty_levels.name'"))
                        {
                            MessageBoxes.Error("Название уровня сложности занято.");
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
                Db.LoadDifficultyLevels();
                fill.listBox.ClearSelected();
                right.Visible = false;
            });
            #endregion
        }

        public void ConfigureOnPuzzles()
        {
            Db.LoadGalleries();
            Db.LoadDifficultyLevels();
            Db.LoadPuzzles();

            Size = normalSize;
            CenterToScreen();
            #region Right
            var right = new ImageAndMethodsControl()
            {
                Dock = DockStyle.Right,
                PanelMethodsVisible = false
            };
            ChangeRight(right);
            #endregion
            #region Fill
            var fill = new UserControls.ListControl()
            {
                Dock = DockStyle.Fill,
            };
            ChangeFill(fill);

            fill.PanelLevelVisible = true;
            fill.comboBoxLevel.DataSource = Db.Instance.DifficultyLevels.Local.ToBindingList();
            fill.comboBoxLevel.DisplayMember = "Name";
            fill.comboBoxLevel.ValueMember = "DifficultyLevelId";

            fill.listBox.DataSource = Db.Instance.Puzzles.Local.ToBindingList();
            fill.listBox.DisplayMember = "Name";
            fill.listBox.ValueMember = "PuzzleId";
            fill.listBox.ClearSelected();
            right.Visible = false;

            fill.comboBoxLevel.SelectedValueChanged += new EventHandler((s, e) =>
            {
                if (fill.comboBoxLevel.SelectedItem == null) return;
                //Db.LoadPuzzles();
                /*
                var ps = Db.Instance.Puzzles.Local;
                foreach (var p in ps)
                {
                    if (p.DifficultyLevel != (DifficultyLevel)fill.comboBoxLevel.SelectedItem)
                    {
                        ps.Remove(p);
                    }
                }*/
            });
            fill.listBox.SelectedValueChanged += new EventHandler((s, e) =>
            {
                DisposeImage(right);
                if (fill.listBox.SelectedItem == null) return;
                right.Visible = true;
                string path = ((Puzzle)fill.listBox.SelectedItem).Image.Path;
                right.pictureBoxImage.Image = LocalStorage.LoadImage(path);
            });
            #endregion
            #region Top
            topControl.ButtonBackVisible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                ConfigureOnAdminMenu();
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
                    Db.Instance.Puzzles.Remove((Puzzle)fill.listBox.SelectedItem);
                    Db.Instance.SaveChanges();
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
                var form = new InsertOrUpdatePuzzleForm
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
                    Db.Instance.PuzzleFields.Load();
                    MyPuzzle.Instance.FragmentNumbers = Db.Instance.PuzzleFields.Find(((Puzzle)fill.listBox.SelectedItem).PuzzleId).FragmentNumbers;
                }
                else
                {
                    Db.Instance.PuzzleTapes.Load();
                    MyPuzzle.Instance.FragmentNumbers = Db.Instance.PuzzleTapes.Find(((Puzzle)fill.listBox.SelectedItem).PuzzleId).FragmentNumbers;
                }
                using var graphics = form.pictureBoxField.CreateGraphics();
                MyPuzzle.Instance.DrawField(graphics);
                form.buttonInsertOrUpdate.Text = "Изменить";
                form.ButtonInsertOrUpdateClick = new EventHandler((s, e) =>
                {
                    try
                    {
                        SetPuzzleDTO(form);

                        var id = new MySqlConnector.MySqlParameter("@id", ((Puzzle)fill.listBox.SelectedItem).PuzzleId);
                        var p1 = new MySqlConnector.MySqlParameter("@p1", PuzzleDTO.Name);
                        var p2 = new MySqlConnector.MySqlParameter("@p2", PuzzleDTO.ImageId);
                        var p3 = new MySqlConnector.MySqlParameter("@p3", PuzzleDTO.DifficultyLevelId);
                        var p4 = new MySqlConnector.MySqlParameter("@p4", PuzzleDTO.FragmentNumbers);
                        int rowsAffected = Db.Instance.Database.ExecuteSqlRaw("CALL `update_puzzle` (@id, @p1, @p2, @p3, @p4)", id, p1, p2, p3, p4);
                        if (rowsAffected != 1)
                        {
                            throw new Exception("Ошибка.");
                        } /*
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
                var form = new InsertOrUpdatePuzzleForm
                {
                    Text = "Добавление пазла",
                };
                form.buttonInsertOrUpdate.Text = "Добавить";
                form.ButtonInsertOrUpdateClick = new EventHandler((s, e) =>
                {
                    try
                    {
                        SetPuzzleDTO(form);

                        var p1 = new MySqlConnector.MySqlParameter("@p1", PuzzleDTO.Name);
                        var p2 = new MySqlConnector.MySqlParameter("@p2", PuzzleDTO.ImageId);
                        var p3 = new MySqlConnector.MySqlParameter("@p3", PuzzleDTO.DifficultyLevelId);
                        var p4 = new MySqlConnector.MySqlParameter("@p4", PuzzleDTO.FragmentNumbers);
                        int rowsAffected = Db.Instance.Database.ExecuteSqlRaw("CALL `insert_puzzle` (@p1, @p2, @p3, @p4)", p1, p2, p3, p4);
                        if (rowsAffected != 1)
                        {
                            throw new Exception("Ошибка.");
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
                Db.LoadDifficultyLevels();
                fill.listBox.ClearSelected();
                right.Visible = false;
            });
            #endregion
        }
    }
}
