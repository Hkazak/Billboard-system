import {baseUrl, LS} from "../Consts";

export const getPriceRuleListEndpoint = `${baseUrl}/PriceRules`;
export const createPriceRuleEndpoint = `${baseUrl}/PriceRules`;

export function GetPriceRulesList()
{
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestInfo = {
        method: 'GET',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${accessToken}`
        },
    };
    return fetch(getPriceRuleListEndpoint, requestInfo);
}

export function CreatePriceRuleRequest(surfaceId, billboardType, price)
{
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestBody = {
        billboardSurfaceId: surfaceId,
        billboardType: billboardType,
        price: price
    };
    const requestInfo = {
        method: 'POST',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${accessToken}`
        },
        body: JSON.stringify(requestBody)
    };
    return fetch(createPriceRuleEndpoint, requestInfo);
}