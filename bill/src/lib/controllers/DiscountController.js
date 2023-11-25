import {baseUrl, LS} from "../Consts";

export const getDiscountsEndpoint = `${baseUrl}/Discounts`;

export function GetDiscounts()
{
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestInfo = {
        method: 'GET',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        },
    };
    return fetch(getDiscountsEndpoint, requestInfo);
}