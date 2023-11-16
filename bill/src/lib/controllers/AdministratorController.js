import { LS, baseUrl } from "../Consts";

export const adminAuthorizationEndpoint = `${baseUrl}/Users/sign-in`;
export const createManagerEndpoint = `${baseUrl}/Managers`;
export const getManagersEndpoint = `${baseUrl}/Managers`;

export async function AuthorizeAdmin(userEmail, userPassword){
    let body = {
        'email': userEmail,
        'password': userPassword
    };

    console.log(body);

    const response = await fetch(adminAuthorizationEndpoint, {
        method: 'POST',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
    });

    return response;
}

export async function CreateManager(email, firstName, lastName, middleName, phone) {
    const accessToken = localStorage.getItem(LS.accessToken);
    let body = {
        "email": email,
        "firstName": firstName,
        "middleName": middleName,
        "lastName": lastName,
        "phone": phone
    }

    console.log(body);

    const response = await fetch(createManagerEndpoint, {
        method: 'POST',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `bearer ${accessToken}`
        },
        body: JSON.stringify(body)
    });

    return response;
}

export async function GetManagersList() {
    const accessToken = localStorage.getItem(LS.accessToken);

    const response = await fetch(getManagersEndpoint, {
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