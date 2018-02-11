namespace BriefRobotics

open System
open System.IO
open System.Net
open System.Net.Http
open System.Text
open Newtonsoft.Json

module Tesla =

    let baseAddress = new Uri("https://owner-api.teslamotors.com/")

    let call get api id token = async {
        use client = new HttpClient(BaseAddress = baseAddress)
        client.DefaultRequestHeaders.Add("Authorization", sprintf "Bearer %s" token)
        let uri = sprintf "api/1/vehicles/%s/%s" id api
        use! response = Async.AwaitTask (if get then client.GetAsync(uri) else client.PostAsync(uri, new StringContent(String.Empty)))
        printfn "CALLING: %s" (sprintf "api/1/vehicles/%s/%s" id api)
        response.EnsureSuccessStatusCode() |> ignore
        let! data = Async.AwaitTask (response.Content.ReadAsStringAsync())
        printfn "RESPONSE: %A" data }

    let command = sprintf "command/%s" >> call false

    let auth (user: string) (password: string) = async {
        use client = new HttpClient(BaseAddress = baseAddress)
        let sb = new StringBuilder()
        let sw = new StringWriter(sb)
        use writer = new JsonTextWriter(sw)
        writer.WriteStartObject();
        writer.WritePropertyName("email")
        writer.WriteValue(user)
        writer.WritePropertyName("password")
        writer.WriteValue(password)
        writer.WritePropertyName("grant_type")
        writer.WriteValue("password")
        writer.WritePropertyName("client_id")
        writer.WriteValue("81527cff06843c8634fdc09e8ac0abefb46ac849f38fe1e431c2ef2106796384")
        writer.WritePropertyName("client_secret")
        writer.WriteValue("c7257eb71a564034f9419ee651c7d0e5f7aa6bfbd18bafb5c5c033b093bb2fa3")
        writer.WriteEndObject();
        use content = new StringContent(sb.ToString(), Encoding.UTF8, "application/json")
        use! response = Async.AwaitTask (client.PostAsync("oauth/token", content))
        response.EnsureSuccessStatusCode() |> ignore
        let! data = Async.AwaitTask (response.Content.ReadAsStringAsync())
        let reader = new JsonTextReader(new StringReader(data))
        let rec readToken () =
            if reader.Read () then
                if reader.TokenType = JsonToken.PropertyName && string reader.Value = "access_token"
                then reader.ReadAsString()
                else readToken ()
            else failwith "Access token not found"
        return readToken () }

    let vehicle (vin: string) (token: string) = async {
        use client = new HttpClient(BaseAddress = baseAddress)
        client.DefaultRequestHeaders.Add("Authorization", sprintf "Bearer %s" token)
        use! response = Async.AwaitTask (client.GetAsync("api/1/vehicles"))
        response.EnsureSuccessStatusCode() |> ignore
        let! data = Async.AwaitTask (response.Content.ReadAsStringAsync())
        let reader = new JsonTextReader(new StringReader(data))
        let rec readId () = // TODO: support multiple vehicles
            if reader.Read () then
                if reader.TokenType = JsonToken.PropertyName && string reader.Value = "id_s"
                then reader.ReadAsString()
                else readId ()
            else failwith "Vehicle ID not found"
        return readId () }

    let mobileEnabled = call true "mobile_enabled"
    let chargeState = call true "data_request/charge_state"
    let climateState = call true "data_request/climate_state"
    let driveState = call true "data_request/drive_state"
    let guiSettings = call true "data_request/gui_settings"
    let vehicleState = call true "data_request/vehicle_state"

    let wakeUp = command "wake_up"
    let setValetMode on password = sprintf "set_valet_mode?on=%b&password=%i" on password |> command
    let flash = command "flash_lights"
    let honk = command "honk_horn"