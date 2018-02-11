# Tesla API Library

# APIs

All APIs require an authentication token (`Authorization: Bearer: ****` header) which is acquired by the `oauth/token` API; given a user name and password to you Tesla account. Most vehicle command and status APIs have a vehicle ID embedded in the URI. This comes from the `id_s` property when listing vehicles (`vehicles` API).

Private content hidden with `****`.

## Authentication

[Client ID and secret](https://pastebin.com/YiLPDggh) may occasionally (likely very seldom) change.

    POST https://owner-api.teslamotors.com/oauth/token
        Content-Type: application/json
        Body:
            {
                "grant_type": "password",
                "client_id": "81527cff06843c8634fdc09e8ac0abefb46ac849f38fe1e431c2ef2106796384",
                "client_secret": "c7257eb71a564034f9419ee651c7d0e5f7aa6bfbd18bafb5c5c033b093bb2fa3",
                "email": "<your email>",
                "password": "<your password>"
            }

    {
        "access_token": "3eec6c35a8795e8f227d8c21c3b5f6686f0bdd1622d4625d2ffc94a77db3****",
        "token_type": "bearer",
        "expires_in": 3888000,
        "refresh_token": "123afbdd97f4cad3a13e7cf502a41da989ed50510c8d4d0404fbbf7a8324****",
        "created_at": 1518369907
    }

## Vehicles

List vehicles under Tesla account; uniquely identifiable by `vin`. The `id_s` property (confusingly, not `vehicle_id`) contains the vehicle ID used in further API calls.

    GET https://owner-api.teslamotors.com/api/1/vehicles
        Authorization: Bearer <your token>

    {
        "response": [
        {
            "id": 5320061548664****,
            "vehicle_id": 115710****,
            "vin": "5YJ3E1EA5HF00****",
            "display_name": "Robot",
            "option_codes": "AD15,MDL3,PBSB,RENA,BT37,ID3W,RF3G,S3PB,DRLH,DV2W,W39B,APF0,COUS,BC3B,CH07,PC30,FC3P,FG31,GLFR,HL31,HM31,IL31,LTPB,MR31,FM3B,RS3H,SA3P,STCP,SC04,SU3C,T3CA,TW00,TM00,UT3P,WR00,AU3P,APH3,AF00,ZCST,MI00,CDM0",
            "color": null,
            "tokens": [
                "bc39e952ffd8****",
                "69816b7da93d****" ],
            "state": "online",
            "in_service": null,
            "id_s": "5320061548664****",
            "remote_start_enabled": true,
            "calendar_enabled": true,
            "notifications_enabled": true,
            "backseat_token": null,
            "backseat_token_updated_at": null
        } ],
        "count": 1
    }

## Mobile Access

    GET https://owner-api.teslamotors.com/api/1/vehicles/<your vehicle id>/mobile_enabled
        Authorization: Bearer <your token>

    {
        "response": true
    }

## Charge State

    GET https://owner-api.teslamotors.com/api/1/vehicles/<your vehicle id>/data_request/charge_state
        Authorization: Bearer <your token>

    {
        "response": {
            "charging_state": "Charging",
            "fast_charger_type": "MCSingleWireCAN",
            "fast_charger_brand": "<invalid>",
            "charge_limit_soc": 81,
            "charge_limit_soc_std": 90,
            "charge_limit_soc_min": 50,
            "charge_limit_soc_max": 100,
            "charge_to_max_range": false,
            "max_range_charge_counter": 1,
            "fast_charger_present": false,
            "battery_range": 200.84,
            "est_battery_range": 0,
            "ideal_battery_range": 200.84,
            "battery_level": 64,
            "usable_battery_level": 64,
            "charge_energy_added": 2.6,
            "charge_miles_added_rated": 11,
            "charge_miles_added_ideal": 11,
            "charger_voltage": 117,
            "charger_pilot_current": 12,
            "charger_actual_current": 12,
            "charger_power": 1,
            "time_to_full_charge": 10.42,
            "trip_charging": false,
            "charge_rate": 4,
            "charge_port_door_open": true,
            "conn_charge_cable": "SAE",
            "scheduled_charging_start_time": null,
            "scheduled_charging_pending": false,
            "user_charge_enable_request": null,
            "charge_enable_request": true,
            "charger_phases": 1,
            "charge_port_latch": "Engaged",
            "charge_current_request": 12,
            "charge_current_request_max": 12,
            "managed_charging_active": false,
            "managed_charging_user_canceled": false,
            "managed_charging_start_time": null,
            "battery_heater_on": false,
            "not_enough_power_to_heat": null,
            "timestamp": 1518333018520
        }
    }

## Climate State

    GET https://owner-api.teslamotors.com/api/1/vehicles/<your vehicle id>/data_request/climate_state
        Authorization: Bearer <your token>

    {
        "response": {
            "inside_temp": 22,
            "outside_temp": 10,
            "driver_temp_setting": 22.2,
            "passenger_temp_setting": 22.2,
            "left_temp_direction": 124,
            "right_temp_direction": 124,
            "is_front_defroster_on": false,
            "is_rear_defroster_on": false,
            "fan_status": 3,
            "is_climate_on": true,
            "min_avail_temp": 15,
            "max_avail_temp": 28,
            "seat_heater_left": 2,
            "seat_heater_right": 2,
            "seat_heater_rear_left": 0,
            "seat_heater_rear_right": 0,
            "seat_heater_rear_center": 0,
            "seat_heater_rear_right_back": 0,
            "seat_heater_rear_left_back": 0,
            "battery_heater": false,
            "battery_heater_no_power": null,
            "is_preconditioning": true,
            "smart_preconditioning": false,
            "is_auto_conditioning_on": true,
            "timestamp": 1518333066108
        }
    }

## Drive State

    GET https://owner-api.teslamotors.com/api/1/vehicles/<your vehicle id>/data_request/drive_state
        Authorization: Bearer <your token>

    {
        "response": {
            "shift_state": "P",
            "speed": null,
            "power": 2,
            "latitude": ****,
            "longitude": ****,
            "heading": 146,
            "gps_as_of": 1518333075,
            "timestamp": 1518333076482
        }
    }

## GUI Settings

    GET https://owner-api.teslamotors.com/api/1/vehicles/<your vehicle id>/data_request/gui_settings
        Authorization: Bearer <your token>

    {
        "response": {
            "gui_distance_units": "km/hr",
            "gui_temperature_units": "C",
            "gui_charge_rate_units": "km/hr",
            "gui_24_hour_time": true,
            "gui_range_display": "Rated",
            "timestamp": 1518333091231
        }
    }

## Vehicle State

Door state is `df` (driver front), `dr` (driver rear), `pf` (passenger front), `pr` (passenger rear). Trunk/frunk state is `ft` (front trunk) and `rt` (rear trunk).

    GET https://owner-api.teslamotors.com/api/1/vehicles/<your vehicle id>/data_request/vehicle_state
        Authorization: Bearer <your token>

    {
        "response": {
            "api_version": 3,
            "autopark_state": "unavailable",
            "autopark_state_v2": "unavailable",
            "calendar_supported": true,
            "car_version": "2017.50.12 b707518",
            "center_display_state": 0,
            "df": 0,
            "dr": 0,
            "ft": 0,
            "locked": true,
            "notifications_supported": true,
            "odometer": 1923.921888,
            "parsed_calendar_supported": true,
            "pf": 0,
            "pr": 0,
            "remote_start": false,
            "remote_start_supported": true,
            "rt": 0,
            "sun_roof_percent_open": null,
            "sun_roof_state": "unknown",
            "timestamp": 1518333098495,
            "valet_mode": false,
            "vehicle_name": "Robot"
        }
    }

## Wake Up

    POST https://owner-api.teslamotors.com/api/1/vehicles/<your vehicle id>/wake_up
        Authorization: Bearer <your token>
    
    {
        "response": {
            "id": 5320061548664****,
            "user_id": 29****,
            "vehicle_id": 115710****,
            "vin": "5YJ3E1EA5HF00****",
            "display_name": "Robot",
            "option_codes": "AD15,MDL3,PBSB,RENA,BT37,ID3W,RF3G,S3PB,DRLH,DV2W,W39B,APF0,COUS,BC3B,CH07,PC30,FC3P,FG31,GLFR,HL31,HM31,IL31,LTPB,MR31,FM3B,RS3H,SA3P,STCP,SC04,SU3C,T3CA,TW00,TM00,UT3P,WR00,AU3P,APH3,AF00,ZCST,MI00,CDM0",
            "color": null,
            "tokens": [
                "7a1af3f27dd0****",
                "9c7f059e4f46****"
            ],
            "state": "online",
            "in_service": null,
            "id_s": "5320061548664****",
            "remote_start_enabled": true,
            "calendar_enabled": true,
            "notifications_enabled": true,
            "backseat_token": null,
            "backseat_token_updated_at": null
        }
    }

## Set Valet Mode

Aside from your vehicle ID, this takes an `on` and `password` (valet pin) parameter.

    POST https://owner-api.teslamotors.com/api/1/vehicles/<your vehicle id>/command/set_valet_mode?on=false&password=1234

    {
        "response": {
            "reason": "already off",
            "result": false
        }
    }

# Reference

* https://timdorr.docs.apiary.io