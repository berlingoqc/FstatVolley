

namespace FormGame

open System.Windows.Forms
open System.Drawing
open System

(*
Form qui prend les infos de depart avant le debut de la game
    -Nom des deux equipes + liste des joueurs avec leur positions depart terrain
    -Ip de la camera (rasberry pi)
*)
type FormAccueil() as form =
    inherit Form()
    
    //Variable prive ?? pas besoin pour sa
    let mutable ListJoueur = List.empty<Joueur>
    let mutable game = {Local="";Visiteur="";Joueurs=ListJoueur}
    //Mes controles
    let lblLocal = new Label()
    let lblVisiteur = new Label()

    let txtLocal = new TextBox()
    let txtVisiteur = new TextBox()

    let txtNom = new TextBox()
    let txtNumero = new TextBox()
    let cbPosition = new ComboBox()

    let btnAjouterJoueur = new Button()
    let lstJoueur = new ListBox()

    let btnConfirmer = new Button()
    
    //Fonction privée


    //Le constructeur qui initialise le formulaire
    do form.InitializeForm

    member this.InitializeForm =
        //Definit les attributs formulaire
        this.Text <- "Création"
        this.Width <- 350
        this.Height <- 450
        //Declare les events du formulaire
        //this.Load.AddHandler(new System.EventHandler
            //(fun s e -> this.Form_Loading(s,e)))
        //this.Closed.AddHandler(new System.EventHandler
            //(fun s e -> this.Form_Closing(s,e)))

        //Modifie les parametres des controles du formulaire
        lblLocal.Text <- "Local"
        lblLocal.Location <- new Point(20,15)

        txtLocal.Location <- new Point(135,15)
        txtLocal.Width <- 135

        lblVisiteur.Text <- "Visiteur"
        lblVisiteur.Location <- new Point(20,55)

        txtVisiteur.Location <- new Point(135,55)
        txtVisiteur.Width <- 135

        txtNom.Location <- new Point(20,95)
        txtNom.Width <- 100
        txtNom.Height <- 20
        
        txtNumero.Location <- new Point(135,95)
        txtNumero.Width <- 25

        for i in 0..6 do cbPosition.Items.Add(i)
        cbPosition.Location <- new Point(170,93)
        cbPosition.Width <- 50
        cbPosition.Height <- 20

        btnAjouterJoueur.Text <- "Ajouter"
        btnAjouterJoueur.Location <- new Point(235,93)
        btnAjouterJoueur.Width <- 75
        btnAjouterJoueur.Height <- 20
        btnAjouterJoueur.Click.AddHandler(new System.EventHandler 
                (fun s e -> this.btnAjouterClick(s, e)))


        lstJoueur.Location <- new Point(20,135)
        lstJoueur.Size <- new Size(290,215)

        btnConfirmer.Text <- "Commencer !"
        btnConfirmer.Location <- new Point(20,365)
        btnConfirmer.Size <- new Size(290,35)
        btnConfirmer.Click.AddHandler(new System.EventHandler
                (fun s e -> this.btnCommencerClick(s,e)))


        //Ajoute le tout au formulaire
        this.Controls.AddRange([|
                                (lblLocal:> Control);
                                (lblVisiteur:> Control);
                                (txtLocal:> Control);
                                (txtVisiteur:> Control);
                                (txtNom:> Control);
                                (txtNumero:> Control);
                                (cbPosition:> Control);
                                (btnAjouterJoueur:> Control);
                                (lstJoueur:> Control);
                                (btnConfirmer:> Control);
                                |])
    //member this.Form_Loading(sender : System.Object, e : EventArgs) =
        //null

    //member this.Form_Closing(sender : System.Object, e : EventArgs) =
         //null
    member this.Game = game
    member this.btnAjouterClick(sender : System.Object, e : EventArgs) =
        let nom = txtNom.Text
        let num = txtNumero.Text
        let pos = cbPosition.SelectedIndex.ToString()
        ListJoueur <- ListJoueur |> List.append [{Nom=nom;Numero=num;Position=Some pos;Stat=None}]
        let ClearLine() =
            txtNom.Clear()
            txtNumero.Clear()
            cbPosition.SelectedIndex = 0
        lstJoueur.Items.Add(nom+"    #"+num)
        let a = ClearLine()
        ListJoueur |> printfn "%A"

    member this.btnCommencerClick(sender : System.Object, e : EventArgs) =
        game <- {Local=txtLocal.Text;Visiteur=txtVisiteur.Text;Joueurs=ListJoueur}
        this.Close()

