import {baseUrl, LS} from "../Consts";

export const createTariffEndpoint = `${baseUrl}/Tariffs`;
export const getTariffListEndpoint = `${baseUrl}/Tariffs`;

export async function SendTariff(title, startTime, endTime, price)
{
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestBody = {
        title: title,
        startTime: startTime,
        endTime: endTime,
        price: parseFloat(price)
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
    return await fetch(createTariffEndpoint, requestInfo);
}

export async function GetTariffs(){
    const accessToken = localStorage.getItem(LS.accessToken);
    const response = await fetch(getTariffListEndpoint, {
        method: 'GET',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        }
    });

    return response;
}
