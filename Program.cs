using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace CadastroDeSeries{
    public class Program{

        [DllImport("kernel32.dll", ExactSpelling = true)]  
        private static extern IntPtr GetConsoleWindow();  
        private static IntPtr ThisConsole = GetConsoleWindow();  
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]  
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);  
        private const int HIDE = 0;  
        private const int MAXIMIZE = 3;  
        private const int MINIMIZE = 6;  
        private const int RESTORE = 9;  
          
        protected static int origRow =  0;
        protected static int origCol = 0;

        protected static int contadorDeSeries = 0;

        public static string EnteredVal = ""; 
        public static string LinhaBrancaText = new String(' ', Console.LargestWindowWidth);
        public static string LinhaBrancaCadastroText = new String(' ', Console.LargestWindowWidth/4);

        public static string LinhaBrancaLimpaCadastro = new String(' ',2 * Console.LargestWindowWidth/3);

        public static string GuestPasswordVal = "XXXXXX";
        public static string EndPasswordVal = "000000";
       
        static SerieRepositorio Repositorio = new SerieRepositorio();
        public static void Main(String[] args)
        {
                    String CoverText = "Sistema de Controle de Series";
                    String VersionText = "Versão DEMO!";
                    String GuestText = "Usuário: Guest User - Senha: XXXXXX ";
                    String PasswordText = "Entre com a senha do USUARIO!";
                    String EndingText = "Para Encerrar digite '000000' e pressione << ENTER >>!";
                    String SeparaText = new String('=', Console.LargestWindowWidth);
                    
                    Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);  
                    ShowWindow(ThisConsole, MAXIMIZE);  
                    
                    ConsoleColor currentBackground = Console.BackgroundColor;
                    ConsoleColor currentForeground = Console.ForegroundColor;
                    
                    ConsoleColor newForeColor = ConsoleColor.White;
                    ConsoleColor newBackColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = newForeColor;
                    Console.BackgroundColor = newBackColor;
                    Console.Clear();
                    
                    int PosxCoverText = Console.LargestWindowWidth/2 - (CoverText.Length/2);
                    int PosyCoverText = Console.LargestWindowHeight/20;
                    WriteText(CoverText,PosxCoverText, PosyCoverText);

                    int PosxVersionText = Console.LargestWindowWidth/2 - (VersionText.Length/2);
                    int PosyVersionText = PosyCoverText + 3;
                    WriteText(VersionText,PosxVersionText, PosyVersionText);

                    int PosxGuestText = Console.LargestWindowWidth/2 - (GuestText.Length/2);
                    int PosyGuestText = PosyVersionText + 3;
                    WriteText(GuestText,PosxGuestText, PosyGuestText);
                    
                    int PosyPasswordText = PosyGuestText + 3;
                    WriteText(PasswordText,origCol, PosyPasswordText);

                    int PosxEndingText = Console.LargestWindowWidth/2 - (EndingText.Length/2);
                    WriteText(EndingText,PosxEndingText, PosyPasswordText+2);
                
                    int PosySeparaText = PosyPasswordText + 3;
                    WriteText(SeparaText,origCol, PosySeparaText);

                    bool success = CheckPassword(PasswordText,origCol,PosyPasswordText);  

                    if (success == false){
                        return;
                    }else{
                        MainMenu();
                        Console.ResetColor();
                        return;
                    }
    
        }

        static uint GetUserChoice(Action printMenu, int choiceMax, int posx, int posy){
            uint choice = 0;
            String SolicitaText = "Opçao Inválida, Por favor, tente novamente!";
            String OpcaoText = "Entre com sua opção: ";
            
            WriteText(OpcaoText,posx, posy);
            Action getInput = () =>
            {
                uint.TryParse(Console.ReadLine(), out choice);
            };
            getInput();
            while ( choice < 1 || choice > choiceMax )
            {
                Console.WriteLine();
                WriteText(SolicitaText,posx, posy + 1);
                Thread.Sleep(2000);
                WriteText(LinhaBrancaText,posx,posy+1);
                WriteText(LinhaBrancaText,posx,posy);
                WriteText(OpcaoText,posx, posy);
                getInput();
            }
            return choice;
        }


        static uint GetUserCadastro(Action printMenu, int choiceMax, int posx, int posy){
                uint cadastro = 0;
                String SolicitaText = "Opçao Inválida, tente novamente!";
                String OpcaoText = "Gênero da Série: ";
                
                WriteText(OpcaoText,posx, posy);
                Action getInput = () =>
                {
                    uint.TryParse(Console.ReadLine(), out cadastro);
                };
                getInput();
                while ( cadastro < 1 || cadastro > choiceMax )
                {
                    Console.WriteLine();
                    WriteText(SolicitaText,posx, posy + 1);
                    Thread.Sleep(2000);
                    WriteText(LinhaBrancaCadastroText,posx,posy+1);
                    WriteText(LinhaBrancaCadastroText,posx,posy);
                    WriteText(OpcaoText,posx, posy);
                    getInput();
                }
                return cadastro;
                }
                            
          static void MainMenu(){

                
                String MenuGeralText1 =  "Bem-Vindo ao Sistema de Controle de SÉRIES!";
                String MenuGeralText2 = "Você tem acesso a todas as operações rotineiras!";
                String MenuGeralText3 = "Versão DEMO! Para utilizar cadastre, PRIMEIRO, pelo menos duas Séries na opção Menu Completo!";
               
                String SeparaText = new String('-', MenuGeralText3.Length + 2);
                
                Action printMenu = () =>
                {
                    Console.WriteLine("Digite 1 Menu Completo");
                    Console.WriteLine("Digite 2 Menu Expresso << apenas saques >> SENHA NÃO HABILITADA!");
                    Console.WriteLine("Digite 3 Menu Expresso << apenas depósitos >> SENHA NÃO HABILITADA!");
                    Console.WriteLine("Digite 4 Menu Expresso << apenas transferências >> SENHA NÃO HABILITADA!");
                    Console.WriteLine("Digite 5 Menu Expresso << apenas extratos >> SENHA NÃO HABILITADA!");
                    Console.WriteLine("Digite 6 para sair");
                };
                
                int PosyMenuGeralText1 = origRow + 16;
                WriteText(SeparaText,origCol, PosyMenuGeralText1 - 1);
                WriteText(MenuGeralText1,origCol, PosyMenuGeralText1);
                WriteText(MenuGeralText2,origCol, PosyMenuGeralText1 + 1);
                WriteText(MenuGeralText3,origCol, PosyMenuGeralText1 + 2);
                WriteText(SeparaText,origCol, PosyMenuGeralText1 + 3);
                Console.WriteLine(" ");
                printMenu();
               
                uint choice = GetUserChoice(printMenu, 6, origCol, origRow + 27);
                switch ( choice )
                {
                    case 1:
                       FullMenu();
                       break;

                    case 2:
                       //ManagerMenu();
                       break;

                    case 4:
                       //CustomerMenu();
                       break;

                    case 5:
                       //ManagerMenu();
                       break;

                    case 6:
                       Console.WriteLine("Obrigado por acessar o Sistema de Controle de Contas Bancárias!, Até Breve!");
                       Thread.Sleep(2000);
                       Console.ResetColor();
                       Environment.Exit(1); 
                       break;
                       

                    default:
                       throw new NotImplementedException();
                    }

                    
        }

        static void FullMenu(){

                String SubMenuText = "Abaixo a lista completa de funcionalidades!";
                String SeparaText = new String('-', SubMenuText.Length + 2);
                String OpcaoText = "Entre com sua opção: ";

                Action printMenu = () =>
                {
                    Console.WriteLine("Digite 1 para CADASTRAR  Séries!");
                    Console.WriteLine("Digite 2 para ATUALIZAR  Séries!");
                    Console.WriteLine("Digite 3 para EXCLUIR    Séries!");
                    Console.WriteLine("Digite 4 para LISTAR     Séries!");
                    Console.WriteLine("Digite 5 para VISUALIZAR Série!");
                    Console.WriteLine("Digite 6 para RETORNAR ao Menu Anterior!");
                };

                int PosySubMenuText = origRow + 34;
                WriteText(SeparaText,origCol, PosySubMenuText - 1);
                WriteText(SubMenuText,origCol, PosySubMenuText);
                WriteText(SeparaText,origCol, PosySubMenuText + 2);
                Console.WriteLine(" ");
                printMenu();
               
                uint choice = GetUserChoice(printMenu, 6, origCol, origRow + 44);
                switch ( choice )
                {
                    case 1:
                        CadastrarNovaSerie();
                        break;
                    case 2:
                        AtualizarSerie();
                        break;
                    case 3:
                        ExcluirSeries();
                        break;
                    case 4:
                        ListarSeries();
                        break;
                    case 5:
                         VisualizarSerie();
                         break;     
                    case 6:

                        for (int indx_ln = -1; indx_ln < 11;indx_ln++){
                         
                         WriteText(LinhaBrancaText,origCol,PosySubMenuText + indx_ln);
                         
                        }

                         WriteText(LinhaBrancaText,origCol,origRow+27);
                         WriteText(OpcaoText,origCol, origRow+27);
                         MainMenu();
                         break;
                    default:
                         throw new NotImplementedException();
                }
        }





////////////////////////////////////   cadastrar nova serie //////////////////////

        private static void CadastrarNovaSerie(){


            String SolicitaText = "Valor Inválido, tente novamente!";
            String SolicitaNome = "Nome Inválido, tente novamente!";
            String OpcaoText = "Entre com sua opção: ";

            
            bool success = false;
            String msg = "Confirma Cadastro de nova Série? < S/s > ou < N/n >";
            Char[] vet = {'S','s','N','n'};
            do{
            
               char key =  GetKeyPress( msg, vet,87 , 28 );

                if ( ( key.Equals('S') )|| (key.Equals('s')) ){

                        WriteText(LinhaBrancaText,origCol,origRow+28);
                        break;

                }else if ( ( key.Equals('N') )|| ( key.Equals('n') ) ){
                
                        WriteText(LinhaBrancaText,origCol,origRow+28);
                        WriteText(LinhaBrancaText,origCol,origRow+44);
                        WriteText(OpcaoText,origCol, origRow+44);
                        FullMenu();
                
                }
            }while(success == false);

            
            Action printCabecalhoCadastro = () =>
                {
                    Console.SetCursorPosition(50,34);
                    Console.WriteLine("Codigo Identificador: ");
                    Console.WriteLine();
                    Console.SetCursorPosition(50,36);
                    Console.WriteLine("Gênero da Série: ");
                    Console.WriteLine();
                    Console.SetCursorPosition(50,38);
                    Console.WriteLine("Título da Série: ");
                    Console.WriteLine();
                    Console.SetCursorPosition(50,40);
                    Console.WriteLine("Ano de Início: ");
                    Console.WriteLine();
                    Console.SetCursorPosition(50,42);
                    Console.WriteLine("Descrição da Série: ");
                    Console.WriteLine();
                    Console.SetCursorPosition(50,44);
                    Console.WriteLine("Situação Operacional: ");
                                       
                };
                printCabecalhoCadastro();

                ++contadorDeSeries;
                int inputNumeroDaSerie = contadorDeSeries;
                
                           
                WriteText (inputNumeroDaSerie.ToString("000000"),72,34);
               
                WriteText("Opções para Gênero:",140,34);

                foreach (int i in Enum.GetValues(typeof(Genero))){
                
                    Console.SetCursorPosition(140,36+i);
                    Console.WriteLine("{0} -- {1}",i, Enum.GetName(typeof(Genero),i));

                }

                uint cadastro = GetUserCadastro(printCabecalhoCadastro,13,50, origRow + 36);
                int inputGeneroDaSerie = (int)cadastro;

                for (int indx_ln = 1; indx_ln < 20;indx_ln++){
                            
                            WriteText(LinhaBrancaCadastroText,127, 32 + indx_ln);
                            
                            
                } 

                                
                bool successNullOrEmpty = true;
                string inputTituloDaSerie = " ";
                var testaValor = " ";

                WriteText ("Título da Série: ", 50,38);
                
                do {
                    testaValor = Console.ReadLine();
                    successNullOrEmpty = IsAnyNullOrEmpty(testaValor);

                    if ( successNullOrEmpty == true ){

                            WriteText(SolicitaNome,50, 39);
                            Thread.Sleep(2000);
                            WriteText(LinhaBrancaCadastroText,50,39);
                            WriteText(LinhaBrancaCadastroText,50,38);
                            WriteText ("Título da Série: ", 50,38);
                            
                    }else{
                    
                        inputTituloDaSerie = testaValor.ToUpperInvariant();
                    
                    }

                }while (successNullOrEmpty == true); 


             //  bool successIsAnyPoint = true;
               bool successValida = false;
               bool isNumConta = false;

                successNullOrEmpty = true;
                success = false;

                int inputAnoDeInicio = 0;
                WriteText ("Ano de Início: ",50,40);

            
               do {

                   try {

                        String testaAno = Console.ReadLine();
                        successValida = ValidaCampo(testaAno, isNumConta);

                        if (successValida){

                           success = int.TryParse(testaAno,out inputAnoDeInicio);

                        }

                     }catch (FormatException){
                    
                    Console.WriteLine("Unable to convert '{0}'.", inputAnoDeInicio);
                
                    }catch (OverflowException){
                    
                    Console.WriteLine("'{0}' is out of range of the double type.", inputAnoDeInicio);
                    }

                    if( !(successValida && success) ){
             
                        WriteText(SolicitaText,50, 41);
                        Thread.Sleep(2000);
                        WriteText(LinhaBrancaCadastroText,50,41);
                        WriteText(LinhaBrancaCadastroText,50,40);
                        WriteText ("Ano de Início: ",50,40);
                                        
                    }
                    
               }while(successValida == false);

                success = true;
                successNullOrEmpty = true;

                string inputDescricaoDaSerie = " ";
                testaValor = " ";
  
                WriteText ("Descrição da Série: ",50,42);
                do{
                    try{
                        testaValor = Console.ReadLine();   
                        successNullOrEmpty = IsAnyNullOrEmpty(testaValor);
                        
                        if ( successNullOrEmpty == false ){

                            inputDescricaoDaSerie = testaValor.ToUpperInvariant();
                        
                        }else{
                        
                            WriteText(SolicitaText,50, 43);
                            Thread.Sleep(2000);
                            WriteText(LinhaBrancaCadastroText,50,43);
                            WriteText(LinhaBrancaCadastroText,50,42);
                            WriteText ("Descrição da Série: ",50,42);
                            
                        }


                    }catch (FormatException){
                    
                    Console.WriteLine("Unable to convert '{0}'.", inputDescricaoDaSerie);
                
                    }catch (OverflowException){
                    
                    Console.WriteLine("'{0}' is out of range of the double type.", inputDescricaoDaSerie);
                    }

                }while( successNullOrEmpty == true ); 


            String inputStatusOperacionalDaSerie = " ";
            bool inputStatus = true;
            

            WriteText ("Status Operacional: ",50,44);
                        
            if( inputStatus == true ){
            
                inputStatusOperacionalDaSerie = "Ativo";
                WriteText (inputStatusOperacionalDaSerie,70,44);
            }
                   
            
            success = true;
            msg = "Digite < S ou s > para fechar Cadastro!";
            
            do{
            
               char key =  GetKeyPress( msg, vet,77,52);

                if ( ( key.Equals('S') )|| (key.Equals('s')) ){

                    
                        for (int indx_ln = 1; indx_ln < 28;indx_ln++){
                                
                                WriteText(LinhaBrancaLimpaCadastro,50, 27 + indx_ln);
                                
                                
                        }
            
                        break;
                }
            }while(success);
            
            Series novaSerie = new Series(id: Repositorio.ProximoId(),
                                        genero: (Genero) inputGeneroDaSerie,
                                        título: inputTituloDaSerie,
                                        ano: inputAnoDeInicio,
                                        descrição: inputDescricaoDaSerie,
                                        excluído: inputStatus);

            Repositorio.InsereSerie(novaSerie);

            WriteText ("Série Cadastrada com Exito! ",80,35);
            Thread.Sleep(2000);
            WriteText(LinhaBrancaCadastroText,80,35);
            WriteText(LinhaBrancaText,origCol,origRow+44);
            WriteText(OpcaoText,origCol, origRow+44);


            FullMenu();
        }



////////////////////// ATUALIZAR ///////////////////

        private static void AtualizarSerie(){


            String SolicitaText = "Valor Inválido, tente novamente!";
            String SolicitaNome = "Nome Inválido, tente novamente!";
            String OpcaoText = "Entre com sua opção: ";

            
            bool success = false;
            String msg = "Confirma Atualizaçao de Dados da Série? < S/s > ou < N/n >";
            Char[] vet = {'S','s','N','n'};
            do{
            
               char key =  GetKeyPress( msg, vet,87 , 28 );

                if ( ( key.Equals('S') )|| (key.Equals('s')) ){

                        WriteText(LinhaBrancaText,origCol,origRow+28);
                        break;

                }else if ( ( key.Equals('N') )|| ( key.Equals('n') ) ){
                
                        WriteText(LinhaBrancaText,origCol,origRow+28);
                        WriteText(LinhaBrancaText,origCol,origRow+44);
                        WriteText(OpcaoText,origCol, origRow+44);
                        FullMenu();
                
                }
            }while(success == false);

            
            Action printCabecalhoCadastro = () =>
                {
                    Console.SetCursorPosition(50,34);
                    Console.WriteLine("Codigo Identificador: ");
                    Console.WriteLine();
                    Console.SetCursorPosition(50,36);
                    Console.WriteLine("Gênero da Série: ");
                    Console.WriteLine();
                    Console.SetCursorPosition(50,38);
                    Console.WriteLine("Título da Série: ");
                    Console.WriteLine();
                    Console.SetCursorPosition(50,40);
                    Console.WriteLine("Ano de Início: ");
                    Console.WriteLine();
                    Console.SetCursorPosition(50,42);
                    Console.WriteLine("Descrição da Série: ");
                    Console.WriteLine();
                    Console.SetCursorPosition(50,44);
                    Console.WriteLine("Situação Operacional: ");
                                       
                };
                printCabecalhoCadastro();

                int inputNumeroDaSerie = 0;
                inputNumeroDaSerie = PegarNumeroDaSerie(50,34);

                           
                WriteText (inputNumeroDaSerie.ToString("000000"),72,34);
               
                WriteText("Opções para Gênero:",140,34);

                foreach (int i in Enum.GetValues(typeof(Genero))){
                
                    Console.SetCursorPosition(140,36+i);
                    Console.WriteLine("{0} -- {1}",i, Enum.GetName(typeof(Genero),i));

                }

                uint cadastro = GetUserCadastro(printCabecalhoCadastro,13,50, origRow + 36);
                int inputGeneroDaSerie = (int)cadastro;

                for (int indx_ln = 1; indx_ln < 20;indx_ln++){
                            
                            WriteText(LinhaBrancaCadastroText,127, 32 + indx_ln);
                            
                            
                } 

                                
                bool successNullOrEmpty = true;
                string inputTituloDaSerie = " ";
                var testaValor = " ";

                WriteText ("Título da Série: ", 50,38);
                
                do {
                    testaValor = Console.ReadLine();
                    successNullOrEmpty = IsAnyNullOrEmpty(testaValor);

                    if ( successNullOrEmpty == true ){

                            WriteText(SolicitaNome,50, 39);
                            Thread.Sleep(2000);
                            WriteText(LinhaBrancaCadastroText,50,39);
                            WriteText(LinhaBrancaCadastroText,50,38);
                            WriteText ("Título da Série: ", 50,38);
                            
                    }else{
                    
                        inputTituloDaSerie = testaValor.ToUpperInvariant();
                    
                    }

                }while (successNullOrEmpty == true); 


             //  bool successIsAnyPoint = true;
               bool successValida = false;
               bool isNumConta = false;

                successNullOrEmpty = true;
                success = false;

                int inputAnoDeInicio = 0;
                WriteText ("Ano de Início: ",50,40);

            
               do {

                   try {

                        String testaAno = Console.ReadLine();
                        successValida = ValidaCampo(testaAno, isNumConta);

                        if (successValida){

                           success = int.TryParse(testaAno,out inputAnoDeInicio);

                        }

                     }catch (FormatException){
                    
                    Console.WriteLine("Unable to convert '{0}'.", inputAnoDeInicio);
                
                    }catch (OverflowException){
                    
                    Console.WriteLine("'{0}' is out of range of the double type.", inputAnoDeInicio);
                    }

                    if( !(successValida && success) ){
             
                        WriteText(SolicitaText,50, 41);
                        Thread.Sleep(2000);
                        WriteText(LinhaBrancaCadastroText,50,41);
                        WriteText(LinhaBrancaCadastroText,50,40);
                        WriteText ("Ano de Início: ",50,40);
                                        
                    }
                    
               }while(successValida == false);

                success = true;
                successNullOrEmpty = true;

                string inputDescricaoDaSerie = " ";
                testaValor = " ";
  
                WriteText ("Descrição da Série: ",50,42);
                do{
                    try{
                        testaValor = Console.ReadLine();   
                        successNullOrEmpty = IsAnyNullOrEmpty(testaValor);
                        
                        if ( successNullOrEmpty == false ){

                            inputDescricaoDaSerie = testaValor.ToUpperInvariant();
                        
                        }else{
                        
                            WriteText(SolicitaText,50, 43);
                            Thread.Sleep(2000);
                            WriteText(LinhaBrancaCadastroText,50,43);
                            WriteText(LinhaBrancaCadastroText,50,42);
                            WriteText ("Descrição da Série: ",50,42);
                            
                        }


                    }catch (FormatException){
                    
                    Console.WriteLine("Unable to convert '{0}'.", inputDescricaoDaSerie);
                
                    }catch (OverflowException){
                    
                    Console.WriteLine("'{0}' is out of range of the double type.", inputDescricaoDaSerie);
                    }

                }while( successNullOrEmpty == true ); 


            String inputStatusOperacionalDaSerie = " ";
            bool inputStatus = true;
            

            WriteText ("Status Operacional: ",50,44);
                        
            if( inputStatus == true ){
            
                inputStatusOperacionalDaSerie = "Ativo";
                WriteText (inputStatusOperacionalDaSerie,70,44);
            }
                   
            
            success = true;
            msg = "Digite < S ou s > para fechar Cadastro!";
            
            do{
            
               char key =  GetKeyPress( msg, vet,77,52);

                if ( ( key.Equals('S') )|| (key.Equals('s')) ){

                    
                        for (int indx_ln = 1; indx_ln < 28;indx_ln++){
                                
                                WriteText(LinhaBrancaLimpaCadastro,50, 27 + indx_ln);
                                
                                
                        }
            
                        break;
                }
            }while(success);
            
            Series novaSerie = new Series(id: (inputNumeroDaSerie-1),
                                        genero: (Genero) inputGeneroDaSerie,
                                        título: inputTituloDaSerie,
                                        ano: inputAnoDeInicio,
                                        descrição: inputDescricaoDaSerie,
                                        excluído: inputStatus);

            Repositorio.AtualizaSerie((inputNumeroDaSerie-1), novaSerie);

            WriteText ("Série Atualizada com Exito! ",80,35);
            Thread.Sleep(2000);
            WriteText(LinhaBrancaCadastroText,80,35);
            WriteText(LinhaBrancaText,origCol,origRow+44);
            WriteText(OpcaoText,origCol, origRow+44);


            FullMenu();
        }

///////////////////////////// LISTAR SERIES///////////////



private static void ListarSeries(){

            
            String OpcaoText = "Entre com sua opção: ";
          
            String msg = "Confirma Listagem  de Dados das Series? < S/s > ou < N/n >";
            Char[] vet = {'S','s','N','n'};
            Char[] list = {'S','s','D','d','X','x'};
            
            bool success = false;
            int inputNumeroDaSerie = 0;

            do{
        
                char key =  GetKeyPress( msg, vet,87 , 28 );

                if ( ( key.Equals('S') )|| (key.Equals('s')) ){

                        WriteText(LinhaBrancaText,origCol,origRow+28);
                        break;

                }else if ( ( key.Equals('N') )|| ( key.Equals('n') ) ){
                
                        WriteText(LinhaBrancaText,origCol,origRow+28);
                        WriteText(LinhaBrancaText,origCol,origRow+44);
                        WriteText(OpcaoText,origCol, origRow+44);
                        FullMenu();
                
                }

        }while(success == false);

        Console.SetCursorPosition(50,28); 
        Console.SetCursorPosition(60,30);   
        Console.WriteLine("Listar séries ");
       
        var lista = Repositorio.ListaDeSeries();

            if(lista.Count == 0)
            {

                WriteText ("Nenhuam série cadastrada!",80,35);
                Thread.Sleep(2000);
                WriteText(LinhaBrancaCadastroText,80,35);
                WriteText(LinhaBrancaText,origCol,origRow+44);
                WriteText(OpcaoText,origCol, origRow+44);
                
                FullMenu();
            }

            int indx = 1;
            foreach (var item in lista)
            {
                var excluido = item.RetornarSituacao();
                if (!excluido)
                {
                    Console.SetCursorPosition(60,30 +  indx);
                    Console.WriteLine("#ID {0} -- Ano: {1}  -- Título: {2}", item.RetornarId(),
                                       item.RetornarAno(), item.RetornarTítulo());
                    ++indx;
                }
                
            }
        

         msg = "Digite < S ou s > para fechar Relatório!";
            
            do{
            
               char key =  GetKeyPress( msg, vet, 77, 52);

                if ( ( key.Equals('S') )|| (key.Equals('s')) ){

                    
                        for (int indx_ln = 1; indx_ln < 28;indx_ln++){
                                
                                WriteText(LinhaBrancaLimpaCadastro,50, 27 + indx_ln);
                                
                                
                        }
            
                        break;
                }
            }while(success);

            WriteText(LinhaBrancaText,origCol,origRow+44);
            WriteText(OpcaoText,origCol, origRow+44);
            
            FullMenu();
           
        }






/////////////////////////////  VISUALIZAR SERIE  ////////////////


private static void VisualizarSerie(){

            
            String OpcaoText = "Entre com sua opção: ";
           // String Cabec1 = "  Id   |    Gênero         |        Título                            |       Perfil          |     Status    |";
           // String Cabec2 = "  Titular                  |   Valor na Conta | Valor Cheque Especial |     Saldo     | ";  
           // String SeparaText = new String('-', Cabec2.Length + 8);
           // String SolicitaText = "Valor Inválido, tente novamente!";

            String msg = "Confirma Exibição de Dados da Serie? < S/s > ou < N/n >";
            Char[] vet = {'S','s','N','n'};
            
            bool success = false;
            int inputNumeroDaSerie = 0;

            do{
        
                char key =  GetKeyPress( msg, vet,87 , 28 );

                if ( ( key.Equals('S') )|| (key.Equals('s')) ){

                        WriteText(LinhaBrancaText,origCol,origRow+28);
                        break;

                }else if ( ( key.Equals('N') )|| ( key.Equals('n') ) ){
                
                        WriteText(LinhaBrancaText,origCol,origRow+28);
                        WriteText(LinhaBrancaText,origCol,origRow+44);
                        WriteText(OpcaoText,origCol, origRow+44);
                        FullMenu();
                
                }

        }while(success == false);

            
        Action printCabecalhoListarSerie = () =>
        {
            Console.SetCursorPosition(50,34);
            Console.WriteLine("Codigo Identificador: ");
            Console.WriteLine();
            Console.SetCursorPosition(50,36);
            Console.WriteLine("Gênero da Série: ");
            Console.WriteLine();
            Console.SetCursorPosition(50,38);
            Console.WriteLine("Título da Série: ");
            Console.WriteLine();
            Console.SetCursorPosition(50,40);
            Console.WriteLine("Ano de Início: ");
            Console.WriteLine();
            Console.SetCursorPosition(50,42);
            Console.WriteLine("Descrição da Série: ");
            Console.WriteLine();
            Console.SetCursorPosition(50,44);
            Console.WriteLine("Situação Operacional: ");
                                
        };
        printCabecalhoListarSerie();
        inputNumeroDaSerie = PegarNumeroDaSerie(50,34);

         
        var serie = (Repositorio.RetornaPorId(inputNumeroDaSerie-1)).ToOpera();
               
        int[] vetIndx = new int[6]; 
        vetIndx = ImprimirItem(serie);

        Console.SetCursorPosition(67,36);
        Console.WriteLine(serie.Substring(vetIndx[0]+1,(vetIndx[1]-vetIndx[0])-1));

        Console.SetCursorPosition(67,38);
        Console.WriteLine(serie.Substring(vetIndx[1]+1,(vetIndx[2]-vetIndx[1])-1));

        Console.SetCursorPosition(65,40);
        Console.Write(serie.Substring(vetIndx[2]+1,(vetIndx[3]-vetIndx[2])-1));

        Console.SetCursorPosition(70,42);
        Console.WriteLine(serie.Substring(vetIndx[3]+1,(vetIndx[4]-vetIndx[3])-1));

        Console.SetCursorPosition(71,44);
        if(serie.Substring(vetIndx[4]+1,(vetIndx[5]-vetIndx[4])-1).Equals("True")){

             Console.WriteLine(" Excluído!");

        }else{
        
            Console.WriteLine(" Ativo!");
        
        }

         msg = "Digite < S ou s > para fechar Relatório!";
            
            do{
            
               char key =  GetKeyPress( msg, vet, 77, 52);

                if ( ( key.Equals('S') )|| (key.Equals('s')) ){

                    
                        for (int indx_ln = 1; indx_ln < 28;indx_ln++){
                                
                                WriteText(LinhaBrancaLimpaCadastro,50, 27 + indx_ln);
                                
                                
                        }
            
                        break;
                }
            }while(success);

            WriteText(LinhaBrancaText,origCol,origRow+44);
            WriteText(OpcaoText,origCol, origRow+44);
            
            FullMenu();
           
        }


/////////////////////////////  EXCLIR SERIE  ////////////////


private static void ExcluirSeries(){

            
            String OpcaoText = "Entre com sua opção: ";
           // String Cabec1 = "  Id   |    Gênero         |        Título                            |       Perfil          |     Status    |";
           // String Cabec2 = "  Titular                  |   Valor na Conta | Valor Cheque Especial |     Saldo     | ";  
           // String SeparaText = new String('-', Cabec2.Length + 8);
           // String SolicitaText = "Valor Inválido, tente novamente!";

            String msg = "Confirma EXCLUSÃO da Serie? < S/s > ou < N/n >";
            Char[] vet = {'S','s','N','n'};
            
            bool success = false;
            int inputNumeroDaSerie = 0;

            do{
        
                char key =  GetKeyPress( msg, vet,87 , 28 );

                if ( ( key.Equals('S') )|| (key.Equals('s')) ){

                        WriteText(LinhaBrancaText,origCol,origRow+28);
                        break;

                }else if ( ( key.Equals('N') )|| ( key.Equals('n') ) ){
                
                        WriteText(LinhaBrancaText,origCol,origRow+28);
                        WriteText(LinhaBrancaText,origCol,origRow+44);
                        WriteText(OpcaoText,origCol, origRow+44);
                        FullMenu();
                
                }

        }while(success == false);

            
        Action printCabecalhoListarSerie = () =>
        {
            Console.SetCursorPosition(50,34);
            Console.WriteLine("Codigo Identificador: ");
            Console.WriteLine();
            Console.SetCursorPosition(50,36);
            Console.WriteLine("Gênero da Série: ");
            Console.WriteLine();
            Console.SetCursorPosition(50,38);
            Console.WriteLine("Título da Série: ");
            Console.WriteLine();
            Console.SetCursorPosition(50,40);
            Console.WriteLine("Ano de Início: ");
            Console.WriteLine();
            Console.SetCursorPosition(50,42);
            Console.WriteLine("Descrição da Série: ");
            Console.WriteLine();
            Console.SetCursorPosition(50,44);
            Console.WriteLine("Situação Operacional: ");
                                
        };
        printCabecalhoListarSerie();
        inputNumeroDaSerie = PegarNumeroDaSerie(50,34);

        Repositorio.ExcluiSerie(inputNumeroDaSerie-1); 
        var serie = (Repositorio.RetornaPorId(inputNumeroDaSerie-1)).ToOpera();

        int[] vetIndx = new int[6]; 
        vetIndx = ImprimirItem(serie);

        Console.SetCursorPosition(67,36);
        Console.WriteLine(serie.Substring(vetIndx[0]+1,(vetIndx[1]-vetIndx[0])-1));

        Console.SetCursorPosition(67,38);
        Console.WriteLine(serie.Substring(vetIndx[1]+1,(vetIndx[2]-vetIndx[1])-1));

        Console.SetCursorPosition(65,40);
        Console.Write(serie.Substring(vetIndx[2]+1,(vetIndx[3]-vetIndx[2])-1));

        Console.SetCursorPosition(70,42);
        Console.WriteLine(serie.Substring(vetIndx[3]+1,(vetIndx[4]-vetIndx[3])-1));

        Console.SetCursorPosition(71,44);
        if(serie.Substring(vetIndx[4]+1,(vetIndx[5]-vetIndx[4])-1).Equals("True")){

             Console.WriteLine(" Excluído!");

        }else{
        
            Console.WriteLine(" Ativo!");
        
        }

         msg = "Digite < S ou s > para fechar Relatório!";
            
            do{
            
               char key =  GetKeyPress( msg, vet, 77, 52);

                if ( ( key.Equals('S') )|| (key.Equals('s')) ){

                    
                        for (int indx_ln = 1; indx_ln < 28;indx_ln++){
                                
                                WriteText(LinhaBrancaLimpaCadastro,50, 27 + indx_ln);
                                
                                
                        }
            
                        break;
                }
            }while(success);

            WriteText(LinhaBrancaText,origCol,origRow+44);
            WriteText(OpcaoText,origCol, origRow+44);
            
            FullMenu();
           
        }





        static bool CheckPassword(string InputText, int posx, int posy)  
                {  
                    try  
                    {  
                        WriteText(InputText,posx,posy);  
                        EnteredVal = "";  

                        do  
                        {  
                            ConsoleKeyInfo key = Console.ReadKey(true);  
                            // Backspace Should Not Work  
                            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter
                               && key.Key != ConsoleKey.Spacebar && key.Key != ConsoleKey.Escape)  
                            {  
                                EnteredVal += key.KeyChar;  
                                Console.Write("*");  
                            }  
                            else  
                            {  
                                if (key.Key == ConsoleKey.Backspace && EnteredVal.Length > 0)  
                                {  
                                    EnteredVal = EnteredVal.Substring(0, (EnteredVal.Length - 1));  
                                    Console.Write("\b \b");  
                                }  
                                else if (key.Key == ConsoleKey.Enter)  
                                {  
                                    if (string.IsNullOrWhiteSpace(EnteredVal))  
                                    {  
                                        Console.WriteLine("");  
                                        WriteText("Senha vazia, não permitido!.",posx,posy+1);
                                        Thread.Sleep(2000);  
                                        WriteText(LinhaBrancaText,posx,posy+1);
                                        WriteText(LinhaBrancaText,posx,posy);
                                        WriteText(InputText,posx,posy);
                                        EnteredVal = "";  
                                        continue;  
                                    }  
                                    else  
                                    {  
                                       
                                        if (EnteredVal.Length != 6 ){

                                            Console.WriteLine("");
                                            Console.WriteLine("Senha invalida! A senha deve ter 6 caracteres!");
                                            Thread.Sleep(2000);
                                            WriteText(LinhaBrancaText,posx,posy+1);
                                            WriteText(LinhaBrancaText,posx,posy);
                                            WriteText(InputText,posx,posy);
                                            EnteredVal = "";  
                                            continue;  

                                        }else if (EnteredVal.Equals(GuestPasswordVal) ){

                                            Console.WriteLine("");
                                            Console.WriteLine("Senha Validada! Aguarde a inicialização do sistema!");
                                            Thread.Sleep(2000);
                                            WriteText(LinhaBrancaText,posx,posy+1);
                                            WriteText(LinhaBrancaText,posx,posy);
                                            WriteText(InputText,posx,posy);
                                            EnteredVal = ""; 
                                            return true;

                                        }else{

                                            if (EnteredVal.Equals(EndPasswordVal) ){
                                                Console.WriteLine("");
                                                Console.WriteLine("Aguarde! Encerrando o sistema!");
                                                Thread.Sleep(2000);
                                                return false;
                                            }else { 

                                                Console.WriteLine("");
                                                Console.WriteLine("Senha Invalida! Não associada a este Usuário!");
                                                Thread.Sleep(2000);
                                                WriteText(LinhaBrancaText,posx,posy+1);
                                                WriteText(LinhaBrancaText,posx,posy);
                                                WriteText(InputText,posx,posy);
                                                EnteredVal = ""; 
                                                continue;

                                            }

                                        } 
                                    }  
                                }  
                            }  
                        } while (true);  
                    }  
                    catch (Exception ex)  
                    {  
                        throw ex;  
                    }  
                }  


        private static int PegarNumeroDaSerie(int col, int row){
        
            String SolicitaSerie  = "Serie Inválida, tente novamente!";
            String NumeroSerieText = "Codigo Identificador: ";
            String OpcaoText = "Entre com sua opção: ";
            
            bool successValida = false;
            bool success = false;
            bool isNumConta = true;

            int inputNumeroDaSerie = 0;
            string testaValor;

            WriteText(NumeroSerieText,col,row);
            
            do{
                try{
                    testaValor = Console.ReadLine();
                    successValida = ValidaCampo(testaValor, isNumConta = true);
              
                    if(successValida){
                        success = Int32.TryParse(testaValor,out inputNumeroDaSerie);
                    }



                }catch (FormatException){
                
                Console.WriteLine("Unable to convert '{0}'.", inputNumeroDaSerie);
            
                }catch (OverflowException){
                
                Console.WriteLine("'{0}' is out of range of the double type.", inputNumeroDaSerie);
                }

                 if ( successValida && success && (inputNumeroDaSerie <= contadorDeSeries) ){

                        
                        WriteText(LinhaBrancaCadastroText,col,row);
                        WriteText (NumeroSerieText,col,row);
                        WriteText (inputNumeroDaSerie.ToString("00000"),col + 22 ,row);
                        break;   
                                    
                   }else{
                   
                    successValida = false;
                    success = false;
                    WriteText(SolicitaSerie,col,row+1);
                    Thread.Sleep(2000);

                    for (int indx_ln = 1; indx_ln < 28;indx_ln++){
                                
                                WriteText(LinhaBrancaLimpaCadastro,50 , 25 + indx_ln);
                    
                    }
                  
                    WriteText(LinhaBrancaText,origCol,origRow+44);
                     WriteText(OpcaoText,origCol, origRow+44);
                    FullMenu();
                   
                   }

            }while (successValida == false);

            return inputNumeroDaSerie;
        
        
        }

       
        private static bool ValidaCampo(string valcampo, bool isNumConta){
            
                    string testaCampo;
                    
                    bool successNullOrEmpty = false;
                    bool successIsAnyNotDigit = false;
                    bool successIsAnyPoint = false;


                    testaCampo = valcampo;   
                    successNullOrEmpty = IsAnyNullOrEmpty(testaCampo);
                    successIsAnyNotDigit = IsAnyNotDigit(testaCampo,isNumConta);
                    successIsAnyPoint = testaCampo.Contains(".");


                    if ( (successNullOrEmpty == false ) && (successIsAnyNotDigit == false )
                       && successIsAnyPoint == false){

                         return true; 

                    }else{
                    
                         return false;
                    }
   
        }



        private static int[] ImprimirItem(string item){

           int end = item.Length;
           int start = 0;
           int count = 0;
           int at = 0;

           int[] vet = new int[6]; 
           int i = 0;

            while((start <= end) && (at > -1))
            {
                // start+count must be a position within -str-.
                count = end - start;
                at = item.IndexOf("|", start, count);
                if (at == -1){
                  break;
                } 
                vet[i] = at;
                //Console.Write("{0} ", at);
                i++;
                start = at+1;
            }  
        
            return vet;
        
        
        }



        static void WriteText(String InputTextToDisplay, Int32 xLocation, Int32 yLocation ){

                       
            bool continueFlag = true;
              

            do {
                
               String textToDisplay = InputTextToDisplay;
               DisplayText(InputTextToDisplay,xLocation,yLocation);
               continueFlag = false;
                
                               
            } while (continueFlag);
        }


         static void DisplayText(String s, Int32 x, Int32 y){
            try
                {

                    Console.SetCursorPosition(origCol+x, origRow+y);
                    if (s.Equals("Conta cadastrada com Exito! ") || 
                        s.Equals("Nenhuma Conta cadastrada! ") ||
                        s.Equals("Deposito realizado com Exito! ") ||
                        s.Equals("Saldo insuficiente para esta operação! ") ||
                        s.Equals("Saque realizado com Exito! ") ){
                    
                        int numflash = 0;
                        while (numflash < 5){
                                
                                    WriteBlinkingText(s, 250, true);
                                    WriteBlinkingText(s, 250, false);
                                    ++numflash;
                            }
                     
                    }else{
                    
                        Console.Write(s);
                    }
                }
            catch (ArgumentOutOfRangeException e)
                {
                Console.Clear();
                Console.Write(e.Message);
                }
            }


        private static void WriteBlinkingText(string text, int delay, bool visible) {
            if (visible)
                Console.Write(text);
            else
                for (int i = 0; i < text.Length; i++)
                    Console.Write(" ");

            Console.CursorLeft -= text.Length;
            System.Threading.Thread.Sleep(delay);
        }




         static Char GetKeyPress(String msg, Char[] validChars,int col, int row)
        {
            ConsoleKeyInfo keyPressed;
            bool valid = false;

            Console.WriteLine();
            do {
                WriteText(msg, col, row);
                keyPressed = Console.ReadKey();
                Console.WriteLine();
                if (Array.Exists(validChars, ch => ch.Equals(Char.ToUpper(keyPressed.KeyChar))))
                    valid = true;
            } while (! valid);
            return keyPressed.KeyChar;
        }



        /// To check the properties of a class for Null/Empty values
        /// </summary>
        /// <param name="obj">The instance of the class</param>
        /// <returns>Result of the evaluation</returns>
        public static bool IsAnyNullOrEmpty(object obj)
        {
            //Step 1: Set the result variable to false;
            bool result = false;

            try
            {
                //Step 2: Check if the incoming object has values or not.
                if (obj != null)
                {
                    //Step 3: Iterate over the properties and check for null values based on the type.
                    foreach (System.Reflection.PropertyInfo pi in obj.GetType().GetProperties())
                    {
                        //Step 4: The null check condition only works if the value of the result is false, whenever the result gets true, the value is returned from the method.
                        if (result == false)
                        {
                            //Step 5: Different conditions to satisfy different types
                            dynamic value;
                            if (pi.PropertyType == typeof(string))
                            {
                                value = (string)pi.GetValue(obj);
                                result = (string.IsNullOrEmpty(value) ? true : false || string.IsNullOrWhiteSpace(value) ? true : false);
                            }
                            else if (pi.PropertyType == typeof(int))
                            {
                                value = (int)pi.GetValue(obj);
                                result = (value <= 0 ? true : false || value == null ? true : false);
                            }
                            else if (pi.PropertyType == typeof(bool))
                            {
                                value = pi.GetValue(obj);
                                result = (value == null ? true : false);
                            }
                            else if (pi.PropertyType == typeof(Guid))
                            {
                                value = pi.GetValue(obj);
                                result = (value == Guid.Empty ? true : false || value == null ? true : false);
                            }
                        }
                        //Step 6 - If the result becomes true, the value is returned from the method.
                        else
                            return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Step 7: If the value doesn't become true at the end of foreach loop, the value is returned.
            return result;
        }


        public static bool IsAnyNotDigit(string campo, bool isNumConta){
        
            //Step 1: Set the result variable to false;
            bool result = false;
            int contVirgula = 0;
           
            try {
                                                              
                    foreach(char c in campo) {    

                        if (c >= '0' && c <= '9'){    
                            continue;   
                        }else{

                            if ( (isNumConta == true) || (contVirgula == 1) ){
                                result = true;    
                                break; 
                            }

                            ++contVirgula;
                                                           
                        }     
                    }     

            }catch (Exception ex){
                throw ex;
            }

            //Step 7: If the value doesn't become true at the end of foreach loop, the value is returned.
            return result;
    
        }

        private static int GeraDigito(int numconta){
        
            int[] intPesos = { 2, 1, 2, 1, 2, 1};
            string strText = numconta.ToString("000000");
    
            if (strText.Length > 6)
                throw new Exception("Número não suportado pela função!");
    
            int intSoma = 0;
            int intIdx = 0;
            for (int intPos = strText.Length - 1; intPos >= 0; intPos--)
            {
                intSoma += Convert.ToInt32(strText[intPos].ToString()) * intPesos[intIdx];
                intIdx++;
            }
    
            intSoma = intSoma % 10;
            intSoma = 10 - intSoma;
            if (intSoma == 10)
            {
                intSoma = 0;
            }
    
            return intSoma;
        }

    }

}
            
    