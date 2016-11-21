#r "System.Net.Http"
#r "System.Net.Http.Formatting"
#r "System.Web.Http"

open System.Net
open System.Net.Http


let proc (req : HttpRequestMessage) = async {
    let! ctn = req.Content.ReadAsStringAsync() |> Async.AwaitTask
    return req.CreateResponse(HttpStatusCode.OK, "Hello" + ctn);
}

let Run(req: HttpRequestMessage) =
    req |> proc |> Async.RunSynchronously