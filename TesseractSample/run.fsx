#r "System.Net.Http"
#r "System.Net.Http.Formatting"
#r "System.Web.Http"
#r "../packages/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"


open System.Net
open System.Net.Http
open Newtonsoft.Json


type Message = {
    name : string
}


let proc (req : HttpRequestMessage) = async {
    let! ctn = req.Content.ReadAsStringAsync() |> Async.AwaitTask
    let msg = JsonConvert.DeserializeObject<Message> ctn


    return req.CreateResponse(HttpStatusCode.OK, "Hello " + msg.name);
}

let Run(req: HttpRequestMessage) =
    req |> proc |> Async.RunSynchronously