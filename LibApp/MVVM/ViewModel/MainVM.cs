using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LibApp.MVVM.Model;
using LibApp.ViewModel.Helpers;
using Microsoft.Win32;
using SerializeManager;

namespace LibApp.MVVM.ViewModel
{
    internal class MainVM : BindingsHelper
    {
        #region Команды
        public BindableCommand ShowModelCommand { get; set; }
        public BindableCommand NextModelCommand { get; set; }
        public BindableCommand PreviosModelCommand { get; set; }

        public BindableCommand OpenFolderCommand { get; set; }  

        #endregion

        #region Свойства
        private ImageModel model;

        public ImageModel Model
        {
            get { return model; }
            set 
            { 
                model = value;
                OnPropertyChanged();
                OnSelectedModelChanged();
            }
        }

        private ObservableCollection<ImageModel> models = new ObservableCollection<ImageModel>();
        public ObservableCollection<ImageModel> Models
        {
            get { return models; }
        }

        private ObservableCollection<ImageModel> selectedModels = new ObservableCollection<ImageModel>();
        public ObservableCollection<ImageModel> SelectedModels
        {
            get { return selectedModels; }
        }
        #endregion
        public MainVM()
        {
            model = new ImageModel();
            OpenFolderCommand = new BindableCommand(_ => OpenFolder());
            ShowModelCommand = new BindableCommand(_ => ShowModelAsJson());
            NextModelCommand = new BindableCommand(_ => NextModel());
            PreviosModelCommand = new BindableCommand(_ => PreviosModel());
        }

        private void ShowModelAsJson()
        {
            MessageBox.Show(SerializeManager.SerializeManager.ConvertModelToJson(Model));
        }

        private void OnSelectedModelChanged()
        {
            selectedModels.Clear();
            selectedModels.Add(model);
        }

        private void NextModel()
        {
            var index = Models.IndexOf(Model);
            Model = index == Models.Count - 1 ? Models.First() : Models[index + 1];
        }

        private void PreviosModel()
        {
            var index = Models.IndexOf(Model);
            Model = index == 0 ? Models.Last() : Models[index - 1];
        }

        private void OpenFolder()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*",
                InitialDirectory = @"C:\",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var fileName in openFileDialog.FileNames)
                {
                    var image = ImageConverter.ImageConverter.ToBitmapImage(fileName);

                    Models.Add(new ImageModel { Name = Path.GetFileName(fileName), Image = image });
                }
            }
        }
    }
}
