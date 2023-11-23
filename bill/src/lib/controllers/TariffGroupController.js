import {baseUrl} from "../Consts";

export const getTariffGroupsEndpoint = `${baseUrl}/GroupOfTariffs`;

export function GetGroupOfTariffsList()
{
    const requestInfo = {
        method: 'GET',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    };
    return fetch(getTariffGroupsEndpoint, requestInfo);
}