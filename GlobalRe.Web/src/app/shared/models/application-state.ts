export class ApplicationState {
    multiSelectionMode: boolean;
    constructor(target: {
        multiSelectionMode: boolean;      
    }) {
        this.multiSelectionMode = target.multiSelectionMode;       
    }
}