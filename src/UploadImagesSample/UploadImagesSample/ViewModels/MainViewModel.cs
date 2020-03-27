using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Threading.Tasks;
using System.Windows.Input;
using UploadImagesSample.Services;
using Xamarin.Forms;

namespace UploadImagesSample.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IBlobServices blobServices;
        private MediaFile _mediaFile;

        public MainViewModel()
        {
            blobServices = DependencyService.Get<IBlobServices>();
        }

        #region Commands
        public ICommand PickPictureCommand => new Command(async () => await PickPicture());
        public ICommand TakePictureCommand => new Command(async () => await TakePicture());
        public ICommand UploadImageCommand => new Command(async () => await Upload());
        #endregion

        #region Properties
        private string responseUrl;

        public string ResponseUrl
        {
            get { return responseUrl; }
            set { responseUrl = value; RaisePropertyChanged(); }
        }

        private ImageSource _image;

        public ImageSource Image
        {
            get { return _image; }
            set { _image = value; RaisePropertyChanged(); }
        }

        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Methods
        private async Task PickPicture()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                ErrorMessage = "This is not support on your device.";
                return;
            }
            else
            {
                PickMediaOptions mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                if (_mediaFile == null) return;

                Path = _mediaFile.Path;
                Image = ImageSource.FromStream(() => _mediaFile.GetStream());
            }
        }
        private async Task TakePicture()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                ErrorMessage = ":( No camera available.";
                return;
            }

            MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "profile.jpg"
            });

            if (file == null)
                return;

            Path = file.Path;

            Image = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }
        private async Task Upload()
        {
            IsBusy = true;
            ResponseUrl = await blobServices.Upload(Path);
            IsBusy = false;
        }

        #endregion
    }
}
