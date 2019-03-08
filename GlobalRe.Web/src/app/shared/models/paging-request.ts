export class PagingRequest<T>{
  searchCriteria: T;
  //(integer - default: 0)
  //Skip over a number of elements by specifying an offset value for the query
  offset?: number;
  //(integer - default: 10)
  //Limit the number of elements on the response (page size)
  limit?: number;
  //Order by field: businessUnitName, inceptionDate, expirationDate, etc...
  sort: string;
  //for exporting, limit is set to total records and export type is set to valid type: Excel or Csv or PDF
  exportType?: string;
  constructor(searchCriteria: T, offset?: number, limit?: number, sort?: string){
    this.searchCriteria = searchCriteria;
    this.offset = offset || 0;
    this.limit = limit || 100;
    this.sort = sort;
  }
}
