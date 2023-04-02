open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection

open Giraffe

type JsonMsg = {
    Msg: string
}

let api = 
    choose[
        GET >=> route "/" >=> text "hello world"
        GET >=> route "/ping" >=> json { Msg = "alive"}
    ]

let configureApp (app : IApplicationBuilder) =
    app.UseGiraffe(api)

let configureServices (services : IServiceCollection) =
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .Configure(configureApp)
                    .UseUrls("http://localhost:8000")
                    .ConfigureServices(configureServices)
                    |> ignore)
        .Build()
        .Run()
    0