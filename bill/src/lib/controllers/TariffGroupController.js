import {baseUrl, LS} from "../Consts";

export const getTariffGroupsEndpoint = `${baseUrl}/GroupOfTariffs`;

export const createGroupOfTariffsEndpoint = `${baseUrl}/GroupOfTariffs`

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

export function CreateGroupOfTariffs(name, tariffs)
{
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestBody = {
        name: name,
        tariffsId: tariffs.map(e=>e.id)
    };
    const requestInfo = {
        method: 'POST',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        },
        body: JSON.stringify(requestBody)
    };
    return fetch(createGroupOfTariffsEndpoint, requestInfo);
}