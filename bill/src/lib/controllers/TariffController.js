import {baseUrl} from "../Consts";

export const createTariffEndpoint = `${baseUrl}/Tariffs`;

export async function SendTariff(title, startTime, endTime, price)
{
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
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBody)
    };
    return await fetch(createTariffEndpoint, requestInfo);
}