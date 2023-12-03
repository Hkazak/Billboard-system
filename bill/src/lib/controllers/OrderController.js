import {baseUrl, LS} from "../Consts";

export const GetBookedOrdersEndpoint = `${baseUrl}/Orders/booked`;
export const CreateOrderEndpoint = `${baseUrl}/Orders`;
export const CalculatePriceEndpoint = `${baseUrl}/Orders/price`;

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

export function CreateOrderRequest(billboardId, startDate, endDate, tariffId, files)
{
    const accessToken = localStorage.getItem(LS.accessToken);
    const startDateString = startDate.toLocaleDateString('ru-RU')
        .replaceAll('/', '-')
        .replaceAll('.', '-');
    const endDateString = endDate.toLocaleDateString('ru-RU')
        .replaceAll('/', '-')
        .replaceAll('.', '-');
    const requestBody = {
        billboardId: billboardId,
        tariffId: tariffId,
        startDate: startDateString,
        endDate: endDateString,
        files: files
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
    }
    return fetch(CreateOrderEndpoint, requestInfo);
}

export function CalculatePriceRequest(billboardId, startDate, endDate, tariffId)
{
    const accessToken = localStorage.getItem(LS.accessToken);
    const startDateString = startDate.toLocaleDateString('ru-RU')
        .replaceAll('/', '-')
        .replaceAll('.', '-');
    const endDateString = endDate.toLocaleDateString('ru-RU')
        .replaceAll('/', '-')
        .replaceAll('.', '-');
    const requestBody = {
        billboardId: billboardId,
        tariffId: tariffId,
        startDate: startDateString,
        endDate: endDateString
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
    }
    return fetch(CalculatePriceEndpoint, requestInfo);
}