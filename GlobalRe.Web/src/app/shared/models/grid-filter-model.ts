export class GridFilterModel{
  columnId:string;
  filterModel:any;
  constructor(target:{columnId:string;
    filterModel:any;})
  {
    this.columnId=target.columnId;
    this.filterModel=target.filterModel;
  }
}
