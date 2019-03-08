//Interface is based on hardcoded JSON Data, will change in future after we get GRS API
export class Documents {
  public Name: string;
  public Contents: Array<Documents>;
  public ID: string;
  public expanded: boolean = false;
  public totalPageCount: number;
  public currentCount: number;
  public isKeyDoc: boolean;
  constructor() {}
  toggle() {
    this.expanded = !this.expanded;
    return this.expanded;
  }
  getIcon() {
    this.expanded ? true : false;
    return this.expanded;
  }
}
