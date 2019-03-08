import {NameValue} from "./name-value";

export class LookupValues {
    url: string; //API URL
    results: NameValue[];

    constructor(url: string, results: NameValue[] ) {
        this.url = url;
        this.results = results;
    }
}

