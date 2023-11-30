import {baseUrl, LS} from "../Consts";

export const getDiscountsEndpoint = `${baseUrl}/Discounts`;
export const createDiscountEndpoint = `${baseUrl}/Discounts`;

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

export function CreateDiscountRequest(name, minRent, discount, endDate, billboards)
{
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestBody = {
        name: name,
        discountPercentage: discount,
        minRentCount: minRent,
        endDate: endDate,
        billboardIds: billboards
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
    return fetch(createDiscountEndpoint, requestInfo);
}