export async function get(path) {
    const request = new Request(path, {
        method: "GET",
        redirect: "error"
    });
    return await fetch(request);
}

export async function getWithHeader(path, headerName, headerVal) {
    var headers = {};
    headers[headerName] = headerVal;

    const request = new Request(path, {
        method: "GET",
        headers: headers
    });

    const response = await fetch(request);
    return response;
}

export async function displayResponse(response) {
    console.log(response.status);
    
    document.getElementById("response-status").innerHTML = response.status;
    document.getElementById("response-content").innerHTML = null;

    const contentType = response.headers.get("content-type");

    if (!response.ok && response.headers.has("www-authenticate")) {
        document.getElementById("response-content").innerHTML = response.headers.get("www-authenticate");
        return;
    }
 
    if (contentType && contentType.indexOf("application/json") !== -1) {
        var data = await response.json();
        document.getElementById("response-content").innerHTML = JSON.stringify(data, null, 4).trim();
        return data;
    } else {
        var data = await response.text();
        document.getElementById("response-content").innerHTML = data;
        return data;
    }
}