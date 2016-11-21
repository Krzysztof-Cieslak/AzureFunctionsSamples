#r "System.Net.Http"
#r "System.Net.Http.Formatting"
#r "System.Web.Http"
#r "System.Drawing"
#r "../packages/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"
#r "../packages/Tesseract/lib/net45/Tesseract.dll"

System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__


open System.Net
open System.Net.Http
open Newtonsoft.Json
open Tesseract


type Message = {
    name : string
}

let ocr input =
    use engine = new TesseractEngine("tessdata", "eng", EngineMode.Default)
    use img = Pix.LoadFromFile("phototest.tif")
    use page = engine.Process img
    page.GetText()


let proc (req : HttpRequestMessage) = async {
    let text = ocr()

    let! ctn = req.Content.ReadAsStringAsync() |> Async.AwaitTask
    let msg = JsonConvert.DeserializeObject<Message> ctn

    let output = sprintf "Hello %s, \nHere is your text: \n%s" msg.name text

    return req.CreateResponse(HttpStatusCode.OK, output);
}

let Run(req: HttpRequestMessage) =
    req |> proc |> Async.RunSynchronously