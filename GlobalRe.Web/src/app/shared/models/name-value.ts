export class NameValue {
    name: string;
    value: string;
    group?: string;
    isActive?:boolean;
    constructor(target: {
        name?: string,
        value?: string,
        group?: string,
        isActive?:boolean;       

    }={} ){
        this.name = target.name;
        this.value = target.value;
        this.group = target.group;
        this.isActive=target.isActive;
    }
}
