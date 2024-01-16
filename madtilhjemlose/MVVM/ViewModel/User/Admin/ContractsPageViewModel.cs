using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.DataAccess;
using madtilhjemlose.MVVM.Model;
using madtilhjemlose.MVVM.View.User.Admin;
using System.ComponentModel;
using System.Windows.Input;
using System;
using System.Drawing;
using System.Drawing.Printing;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Maui.Controls.Shapes;
using System.Drawing.Text;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal class ContractsPageViewModel : ObservableValidator, INotifyPropertyChanged
    {
        private INavigation navigation;
        protected static ContractRepository repository = [];
        private Contract _contract;

        public ICommand CreatePDFCommand { get; set; }
        public ICommand CreateContractCommand { get; set; }

        public ContractsPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            CreatePDFCommand = new RelayCommand(CreatePDF);
            CreateContractCommand = new RelayCommand(CreateContract);
            ContractList = GetAllContracts();
            _contract = ContractList[0];
        }

        private void CreateContract()
        {
            navigation.PushAsync(new CreateContractPage());
        }

        private void CreatePDF() // Jesper og Jannick
        {
            // where the file is saved to
            string filePath = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +"\\" + _contract.CompanyName.Trim() + "contract.pdf";

            // Check if the file already exists
            if (File.Exists(filePath))
            {
                return;
            }
            try
            {
                PdfDocument pdfDocument = new PdfDocument();

                // Add a page to the document
                PdfPage page = pdfDocument.AddPage();

                // Create a graphics object for the page
                XGraphics gfx = XGraphics.FromPdfPage(page);

                InstalledFontCollection installedFontCollection = new InstalledFontCollection();
                string chosenFont = "";
                foreach (FontFamily fontFamily in installedFontCollection.Families) 
                { if (fontFamily.Name == "Calibri") {chosenFont = fontFamily.Name; break; } }
                
                // Create a font for the text
                //XFont font = new XFont("Arial", 12, XFontStyleEx.Regular);
                XFont font = new XFont(chosenFont.ToString(), 12,XFontStyleEx.Regular);

                DateTime currentDate = DateTime.Now;

                List<string> lines = new List<string>();
                lines.Add("PDF creation date : " + currentDate.ToString());
                lines.Add("Company name : " + _contract.CompanyName);
                lines.Add("Company contract id : " + SelectedContractID.ToString());
                lines.Add("Contract start date : " + SelectedStartDate.ToString());
                lines.Add("Contract end date : " + SelectedEndDate.ToString());
                lines.Add("Company address : " + SelectedCompanyAddress.ToString());

                for(int i = 0; i < lines.Count; i++)
                {
                    // Draw the text on the PDF
                    gfx.DrawString(lines[i].ToString(), font, XBrushes.Black, 100, (i+1) * 50);
                    // note: 100 and (i+1)*50 are the x,y coordinates
                }
                // Save the document to the specified path
                pdfDocument.Save(filePath);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Contract> ContractList { get; set; }
        private List<Contract> GetAllContracts()
        {
            repository.Search(string.Empty);
            var contracts = new List<Contract>();

            foreach (var contract in repository)
            {
                contracts.Add(contract);
            }

            // get all contract names from db and fill it in this.contracts 
            // Order by asending 

            return contracts;
        }
        
        public Contract SelectedContract
        {
            get { return _contract; }
            set 
            {
                if(_contract != value)
                {

                    // updates values when new item from picker is chosen.
                    _contract = value;
                    OnPropertyChanged(nameof(SelectedContract));
                    OnPropertyChanged(nameof(SelectedContractID));
                    OnPropertyChanged(nameof(SelectedStartDate));
                    OnPropertyChanged(nameof(SelectedEndDate));
                    OnPropertyChanged(nameof(SelectedCompanyAddress));


                }
            }
        }
        
        public int SelectedContractID
        {
            get { return _contract.ContractID; }
            set
            {
                if( _contract.ContractID != value)
                {
                    _contract.ContractID = value;
                    OnPropertyChanged(nameof(SelectedContractID));
                }
            }
        }

        public string SelectedStartDate
        {
            get
            {
                return _contract.ContractStart;
            }
            set
            {
                if ( _contract.ContractStart != value)
                {
                    _contract.ContractStart = value;
                    OnPropertyChanged(nameof(SelectedStartDate));
                }
            }
        }

        public string SelectedEndDate
        {
            get => _contract.ContractEnd;
            set
            {
                if (_contract.ContractEnd != value)
                {
                    _contract.ContractEnd = value;
                    OnPropertyChanged(nameof(SelectedEndDate));
                }
            }
        }

        public string SelectedCompanyAddress
        {
            get => _contract.CompanyAddress;
            set
            {
                if( value != _contract.CompanyAddress)
                {
                    _contract.CompanyAddress = value;
                    OnPropertyChanged(nameof(SelectedCompanyAddress));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
