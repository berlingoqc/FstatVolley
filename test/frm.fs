


namespace FormGame
open System.Windows.Forms
open System.Drawing


type frmGetInfo(s:string list) as frm =
    inherit Form()
    
    let btn1 = new Button()
    let btn2 = new Button()
    let btn3 = new Button()
    let btn4 = new Button()
    let btn5 = new Button()
    let btn6 = new Button()
    let btn7 = new Button()
    let btn8 = new Button()
    let listBtn = [btn1;btn2;btn3;btn4;btn5;btn6;btn7;btn8]

    let position = [for i in 0..59..(s.Length*59)-1 do yield new Point(i,0)]
    let namPos = List.map2 (fun x y -> (x,y)) s position 

    let sizebtn = new Size(59,59)

    do frm.Size <- new Size((s.Length)*62,97)

    do frm.StartPosition <- FormStartPosition.CenterScreen

    let mutable ValeurRetour = ""

    do frm.Init()

    member this.Retour = ValeurRetour

    member this.Init() = 
       this.Visible = false
       let l = listBtn |> List.skip(8-s.Length)
       List.iter2 (fun (x:Button) (y:string*Point) -> 
                x.Name <- fst y;
                          x.Text <- fst y;
                          x.Name <- fst y;
                          x.Location <- snd y;
                          x.Size <- sizebtn;
                          x.Click.AddHandler(fun s _ -> this.btnClick(s:?>Button));
                          x.Click.AddHandler(fun _ _ -> this.Close())) l namPos
       l |> List.iter (fun x -> this.Controls.Add(x:>Control))

     member this.btnClick(s:Button) =
        ValeurRetour <- s.Text


        
