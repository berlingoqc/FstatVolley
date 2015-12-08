namespace FormGame

open System.Windows.Forms
open System.Reflection
open System.Drawing
open System

type FormMain(Game) as form =
    inherit Form()

    let gameinfo = StatGame(Game)

    let mutable doPaint = false
    let mutable ligne = List.empty<Point>
    let mutable listligne = List.empty<Point list>

    //Petite fonction privée
    let SupLigne() = listligne <- (List.skip(1) listligne);
            


    let terrain = new FormGame.TerrainVolley()


    //Label pour les timers
    let lblTitreGame = new Label()
    let lblTGame = new Label()
    let lblTitreSet = new Label()
    let lblTSet = new Label()
    let lblTitrePoint = new Label()
    let lblTPoint = new Label()

    //Bouton Pour Supprimer la premier ligne du table de ligne
    let btnSupLigne = new Button()
    //Label set
    let lblSet = new Label()

    //titre visiteur et local
    let lblVisiteur = new Label()
    let lblLocal = new Label()

    //Button Pour commencer Point
    let btnStartPoint = new Button()

    //DataGrid pour les statistiques avec combobox selectionner par set
    let cbSetStat = new ComboBox()
    let btnSelectSet = new Button()

    let gridStat = new DataGrid()

    let lblPointVisiteur = new Label()
    let lblPointLocal = new Label()

    let btnSup = new Button()
    let lstBanc = new ListBox()
    let lstJoueurTerrain = new ListBox()

    let btnKill = new Button()
    let btnBloc = new Button()
    let btnReception = new Button()
    let btnService = new Button()
    let btnDig = new Button()
    let btnSet = new Button()
    let listBtn = [btnKill;btnBloc;btnReception;btnService;btnDig;btnSet]


    do form.InitializeForm

    member this.InitializeForm =
        //Definit les attributs formulaire
        this.Text <- gameinfo.Local + "    versus    " + gameinfo.Visiteur
        this.Size <- new Size(1031,518)

        btnSup.Size <- new Size(120,23)
        btnSup.Location <- new Point(20,242)
        btnSup.Text <- "Subtitution"

        lstBanc.Size <- new Size(120,173)
        lstBanc.Location <- new Point(20,280)
        for i in gameinfo.Banc() do lstBanc.Items.Add(i.Nom)

        lstJoueurTerrain.Size <- new Size(120,173)
        lstJoueurTerrain.Location <- new Point(341,280)
        for i in gameinfo.Court() do lstJoueurTerrain.Items.Add(i.Nom)


        lblPointLocal.Text <- "0"
        lblPointLocal.Name <- "local"
        lblPointLocal.Location <- new Point(797,10)
        lblPointLocal.Size <- new Size(57,39)
        lblPointLocal.Font <- new Font("Microsoft Sans Serif",float32 32)


        lblPointVisiteur.Text <- "0"
        lblPointVisiteur.Name <- "visiteur"
        lblPointVisiteur.Location <- new Point(895,10)
        lblPointVisiteur.Size <- new Size(57,39)
        lblPointVisiteur.Font <- new Font("Microsoft Sans Serif",float32 32)

        lblTitreGame.Text <- "Time in  Game:"
        lblTitreGame.Location <- new Point(459,16)
        lblTitreGame.Size <- new Size(75,13)

        lblTGame.Text <- "0 min"
        lblTGame.Location <- new Point(542,16)
        
        lblTitreSet.Text <- "Set"
        lblTitreSet.Location <- new Point(500,36)
        lblTitreSet.Size = new Size(20,13)

        lblTSet.Text <- "0min0sec"
        lblTSet.Location <- new Point(537,36)
        lblTSet.Size <- new Size(38,13)

        lblTitrePoint.Text <- "Point"
        lblTitrePoint.Location <- new Point(500,57)
        lblTitrePoint.Size = new Size(20,13)

        lblTPoint.Text <- "0min0sec"
        lblTPoint.Location <- new Point(537,57)

        cbSetStat.Location <- new Point(20,28)
        cbSetStat.Size <- new Size(121,21)

        btnSelectSet.Text <- "Selectionner"
        btnSelectSet.Location <- new Point(147,26)
        btnSelectSet.Size <- new Size(75,23)

        lblVisiteur.Text <- gameinfo.Visiteur
        lblVisiteur.Location <- new Point(899,8)

        lblLocal.Text <- gameinfo.Local
        lblLocal.Location <- new Point(801,8)

        btnSupLigne.Text <- "Annuler premiere ligne"
        btnSupLigne.Location <- new Point(645,42)
        btnSupLigne.Size <- new Size(110,28)

        btnStartPoint.Text <- "Start Point"
        btnStartPoint.Location <- new Point(401,81)
        btnStartPoint.Size <- new Size(60,59)

        terrain.Size <- new Size(536,536/2)
        terrain.Location <- new Point(467,131)
        terrain.MouseMove.AddHandler(
            (fun s e -> this.terrainMouseMove(s,e)))
        terrain.MouseDown.AddHandler(
            (fun s _ -> this.terrainMouseDown(s)))
        terrain.MouseUp.AddHandler(
            (fun s _ -> this.terrainMouseUp(s)))

         //Configurer la data grid XD
        gridStat.Location <- new Point(20,57)
        gridStat.Size <- new Size(363,150)
        

        let strname = [("Kill",new Point(146,299));("Bloc",new Point(211,299));("Reception",new Point(276,299));
                        ("Service",new Point(146,377));("Dig",new Point(211,377));("Set",new Point(276,377));]
        this.InitButtons(listBtn,strname,new Size(59,59))
        
        btnKill.Click.AddHandler(
            (fun s _ -> this.btnActionClick(s)))

        let gf = gameinfo.Court()
        let gff = gameinfo.Banc()

        for i in listBtn do this.Controls.Add(i:>Control)

        
        this.Controls.AddRange([|
                                (lblTitreGame:>Control);
                                (lblTGame:>Control);
                                (lblTitreSet:>Control);
                                (lblTSet:>Control)
                                (lblTitrePoint:>Control);
                                (lblTPoint:>Control);
                                (lblVisiteur:>Control);
                                (lblLocal:>Control);
                                (btnSupLigne:>Control)
                                (cbSetStat:>Control);
                                (btnSelectSet:>Control);
                                (gridStat:>Control);
                                (btnSup:> Control);
                                (lstBanc:> Control);
                                (lstJoueurTerrain:> Control)
                                (lblPointLocal:> Control);
                                (lblPointVisiteur:> Control);
                                (terrain:> Control);
                                |])
        terrain.DrawTerrain()
    //Fonction utilitaire
    member this.InitButtons (lstBtn:Button list, strname:(string*Point) list, size:Size) = 
        List.iter2 (fun (x:Button) (y:(string*Point)) -> 
                x.Name <- fst y;
                          x.Text <- fst y;
                          x.Name <- fst y;
                          x.Location <- snd y;
                          x.Size <- size) listBtn strname

    //Event handler dessiner ligne frappe terrain
    member this.terrainMouseDown(sender : System.Object) =
        doPaint <- true 

    member this.terrainMouseUp(sender : System.Object) = 
        //Ajoute la liste de point au tableau de list de point
        listligne <- List.append listligne [ligne]
        printfn "%A" listligne
        terrain.Paint.Add(fun x -> x.Graphics.Clear(this.BackColor))
        //Vide la liste
        ligne <- List.empty<Point>
        doPaint <- false

    member this.terrainMouseMove(sender : System.Object, e : MouseEventArgs) = 
        match doPaint with
        | true -> terrain.DrawPoint(e.Location); ligne <- List.append ligne [e.Location]
        | _ -> ()
    
    member this.AskInfo(s:string list) =
        let frm = new FormGame.frmGetInfo(s)
        frm.ShowDialog()
        frm.Retour    
    //Event handler pour les Click
    member this.btnActionClick(sender : System.Object) =
        //Le nom du joueur selectioner sur le terrain
        let name = 
            match lstJoueurTerrain.SelectedItem.ToString() with
            | null -> ""
            | name -> name
        //Demande avec un form le type ex : frappe ( kill touch , ...)   
        match (sender:?>Button).Name with
        | "Kill" -> gameinfo.AjouterFrappe(name,(this.AskInfo(M.Attack())|> fun x -> FormGame.M.Attack(x)),5.,listligne.Head)
        | "Bloc" -> gameinfo.AjouterBloc(name,(this.AskInfo(M.Bloc()) |> fun x -> FormGame.M.Bloc(x)),5.)
