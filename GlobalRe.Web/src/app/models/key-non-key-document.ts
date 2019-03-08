export class KeyNonKeyDocument {
  docid?: string;
  docName?: string;
  docType?: string;
  public expanded: boolean = false;
  public totalPageCount: number;
  public currentCount: number;
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
