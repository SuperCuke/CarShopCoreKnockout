import 'isomorphic-fetch';

export async function get<T>(url: string): Promise<T> {
    var response = await fetch(url);
    var result = await response.json();
    return result as T;
}