import {LS, baseUrl} from "../Consts";

export const managerSignInEndpoint = `${baseUrl}/Managers/sign-in`;
export const userResetPasswordSendEmailEndpoint = `${baseUrl}/Users/password/forgot`;
export const userResetPasswordChangePasswordEndpoint = `${baseUrl}/Users/password/reset`;
export const activateManagerEndpoint = `${baseUrl}/Managers/activate`
export const deactivateManagerEndpoint = `${baseUrl}/Managers`
export const createManagerEndpoint = `${baseUrl}/Managers`;

export async function AuthorizeManager(userEmail, userPassword) {
    let body = {
        'email': userEmail,
        'password': userPassword
    };

    console.log(body);

    const response = await fetch(managerSignInEndpoint, {
        method: 'POST',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
    });

    localStorage.setItem(LS.userRole, 'manager');
    return response;
}

export function CreateManagerRequest(firstName, middleName, lastName, email, phone) {
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestBody = {
        email: email,
        firstName: firstName,
        middleName: middleName,
        lastName: lastName,
        phone: phone
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
    return fetch(createManagerEndpoint, requestInfo);
}

export async function ActivateManager(id) {
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestInfo = {
        method: 'PATCH',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${accessToken}`
        },
    };
    return fetch(`${activateManagerEndpoint}/${id}`, requestInfo);
}

export async function DeactivateManager(id) {
    const accessToken = localStorage.getItem(LS.accessToken);
    const requestInfo = {
        method: 'DELETE',
        headers: {
            "Access-Control-Allow-Origin": "*",
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${accessToken}`
        },
    };
    return fetch(`${deactivateManagerEndpoint}/${id}`, requestInfo);
}