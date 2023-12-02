import {baseUrl, LS} from "../Consts";

export const GetBookedOrdersEndpoint = `${baseUrl}/Orders/booked`;

export function GetBookedOrdersRequest(billboardId, tariffId = null)
{
    let endpoint = `${GetBookedOrdersEndpoint}?billboardId=${billboardId}`;
    if(tariffId !== null)
    {
        endpoint = `${endpoint}&tariffId=${tariffId}`;
    }
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestInfo = {
        method: 'GET',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        }
    }
    return fetch(endpoint, requestInfo);
}