open System
open BriefRobotics

[<EntryPoint>]
let main argv =
    printfn "Tesla Demo"
    let car = new Tesla("<your user>", "<your password>", "<your vin>")
    car.ChargeState() |> printfn "ChargeState: %s"
    car.FlashLights() |> ignore
    car.HonkHorn()
    car.AutoConditioningStart() |> ignore
    0