import 'isomorphic-fetch';

export async function get<T>(url: string): Promise<T> {
    var response = await fetch(url);
    var result = await response.json();
    return result as T;
}

export async function post<T>(url: string, data: any): Promise<T> {
    var response = await fetch(url, {
        method: "POST",
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });
    var result = await response.json();
    return result as T;
}