

namespace FormGame

open System.Windows.Forms
open System.Drawing
open System

type FormMain(Game) as form =
    inherit Form()

    let Local = Game.Local
    let Visiteur = Game.Visiteur
    let Joueurs = Game.Joueurs

    let mutable doPaint = false
    let mutable ligne = List.empty<Point>

    let terrain = new Panel()

    let lblPointVisiteur = new Label()
    let lblPointLocal = new Label()

    let btnSup = new Button()
    let lstBanc = new ListBox()

    do form.InitializeForm

    member this.InitializeForm =
        //Definit les attributs formulaire
        this.Text <- Local + "    versus    " + Visiteur
        this.Size <- new Size(1022,518)

        btnSup.Size <- new Size(120,23)
        btnSup.Location <- new Point(323,246)
        btnSup.Text <- "Subtitution"
        btnSup.Click.AddHandler(
            (fun s _ -> this.btnSupClick(s)))

        lstBanc.Size <- new Size(120,173)
        lstBanc.Location <- new Point(323,275)

        lblPointLocal.Text <- "0"
        lblPointLocal.Name <- "local"
        lblPointLocal.Location <- new Point(625,6)
        lblPointLocal.Size <- new Size(57,39)
        lblPointLocal.Font <- new Font("Microsoft Sans Serif",float32 32)
        lblPointLocal.Click.AddHandler(
                (fun s _ -> this.lblPointClick(s)))

        lblPointVisiteur.Text <- "0"
        lblPointVisiteur.Name <- "visiteur"
        lblPointVisiteur.Location <- new Point(799,6)
        lblPointVisiteur.Size <- new Size(57,39)
        lblPointVisiteur.Font <- new Font("Microsoft Sans Serif",float32 32)
        lblPointVisiteur.Click.AddHandler(
                (fun s _ -> this.lblPointClick(s)))


        terrain.Size <- new Size(536,536/2)
        terrain.Location <- new Point(458,116)
        terrain.MouseMove.AddHandler(
            (fun s e -> this.terrainMouseMove(s,e)))
        terrain.MouseDown.AddHandler(
            (fun s _ -> this.terrainMouseDown(s)))
        terrain.MouseUp.AddHandler(
            (fun s _ -> this.terrainMouseUp(s)))

        this.Controls.AddRange([|
                                (btnSup:> Control);
                                (lstBanc:> Control);
                                (lblPointLocal:> Control);
                                (lblPointVisiteur:> Control);
                                (terrain:> Control);
                                |])
        this.DessinTerrain()
    //Fonction utilitaire
    member this.Draw (g:Graphics) (pen:Pen) (points:PointF list) =
        for i in 0..2..(points.Length-1) do
            g.DrawLine (pen, points.[i],points.[i+1])
                
    member this.DessinTerrain() =
        let pen = new Pen(brush=Brushes.Black, width = 5.f)
        let lenX = 536.0f
        let points = ([|new PointF(0.0f,0.0f);new PointF(lenX,0.0f);
                        new PointF(lenX,0.0f);new PointF(lenX,lenX/2.f);
                        new PointF(0.0f,0.0f);new PointF(0.0f,lenX/2.f);
                        new PointF(0.0f,lenX/2.0f);new PointF(lenX,lenX/2.f);
                        new PointF(lenX/2.f,0.0f);new PointF(lenX/2.0f,lenX/2.f);
                        new PointF(lenX/2.f-lenX/6.f,0.0f);new PointF(lenX/2.f-lenX/6.f,lenX/2.f);
                        new PointF(lenX/2.f+lenX/6.f,0.0f);new PointF(lenX/2.f+lenX/6.f,lenX/2.f)|])
        terrain.Paint.Add(
            fun e ->
                points
                |> List.ofArray
                |> this.Draw e.Graphics pen)
        printfn ""

    //Event handler dessiner ligne frappe terrain
    member this.terrainMouseDown(sender : System.Object) =
        doPaint <- true 

    member this.terrainMouseUp(sender : System.Object) = 
        //Ajoute la liste de point a la statistique pour cree un vector ou je sais pas quoi

        doPaint <- false

    member this.terrainMouseMove(sender : System.Object, e : MouseEventArgs) = 
        match doPaint with
        | true -> ligne <- List.append ligne [e.Location]
        | _ -> ()
        printfn "%A" ligne
        
        
        

    //Event handler pour les Click
    member this.btnSupClick(sender : System.Object) =
        printfn ""

    member this.lblPointClick(sender : System.Object) =
        match (sender:?>Label).Name with
        | "local" -> printfn "Point"
        | "visiteur" -> printfn "das"
        | _ -> ()