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

        private void CreatePDF() // Jesper - This code was inspired from ChatGPT's help  
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
                foreach (FontFamily fontFamily in installedFontCollection.Families) { if (fontFamily.Name == "Calibri") {chosenFont = fontFamily.Name; break; } }
                
                // Create a font for the text
                //XFont font = new XFont("Arial", 12, XFontStyleEx.Regular);
                XFont font = new XFont(chosenFont.ToString(), 12,XFontStyleEx.Regular);

                // Draw the text on the PDF
                gfx.DrawString("This is a line in the PDF.", font, XBrushes.Black, 100, 100);

                // Save the document to the specified path
                pdfDocument.Save(filePath);

                //Console.WriteLine($"PDF created at: {System.IO.Path.GetFullPath(filePath)}");
            }
            catch (Exception)
            {

                throw;
            }

            /*
        try
        {

            // Create a PrintDocument object
            System.Drawing.Printing.PrintDocument pd = new PrintDocument();
            // Set the PrintPage event handler
            pd.PrintPage += (sender, e) =>
            {
                DateTime currentDate = DateTime.Now;

                List<string> lines = new List<string>();
                lines.Add("PDF creation date : " + currentDate.ToString());
                lines.Add("Company name : " + _contract.CompanyName);
                lines.Add("Company contract id : " + SelectedContractID.ToString());
                lines.Add("Contract start date : " + SelectedStartDate.ToString());
                lines.Add("Contract end date : " + SelectedEndDate.ToString());
                lines.Add("Company address : " + SelectedCompanyAddress.ToString());

                System.Drawing.Font font = new System.Drawing.Font("Arial", 12);
                SolidBrush brush = new SolidBrush(System.Drawing.Color.White);

                foreach (string line in lines)
                {
                    e.Graphics.DrawString(line, font, brush, 50, 50);
                }

            };
            // Save the document as a PDF file
            pd.PrintController = new StandardPrintController();
            pd.PrinterSettings.PrintToFile = true;
            pd.PrinterSettings.PrintFileName = filePath;

            // Print the document (this will trigger the PrintPage event)
            pd.Print();
        }
        catch (Exception ex)
        {

            throw;
        }
        */
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
