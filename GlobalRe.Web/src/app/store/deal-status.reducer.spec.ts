
import {reducer,initialState} from './deal-status.reducer';
import * as fromDealStatus from './deal-status.reducer';
import { DealStatusActions, GetDealStatusSuccessAction,
  GetDealStatusAction,GetInprogresDealCountSuccessAction, UpdateDealStatusAction, UpdateSubDealStatusAction } from '../actions/deals/deal-status.actions';
import{ DealStatus } from '../models/deal-status';
import {EntityApiData} from '../shared/models/entity-api-data';

const dealsCount:any=
       [{ data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
           statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
           links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
      },{ data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 15, statusName: "On Hold Test", sortOrder: 2, count: 28,
           statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 18, statusName: "On Hold Test", sortOrder: 2, count: 28,} ]},
         links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
       },{ data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 14, statusName: "On Hold Zero", sortOrder: 2, count: 0,
           statusSummary:[]},
         links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
       }];

const dealsList:any={"dealsCount":
       [{ data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
          statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
          links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
       },{ data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 15, statusName: "On Hold Test", sortOrder: 2, count: 28,
           statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 18, statusName: "On Hold Test", sortOrder: 2, count: 28,} ]},
          links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
       },{ data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 14, statusName: "On Hold Zero", sortOrder: 2, count: 0,
           statusSummary:[]},
           links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
       }]};

const expectedmockdealStatusResult: any={"dealsCount":
      [ {
        data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
          statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
        links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
      },{
        data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 15, statusName: "On Hold Test", sortOrder: 2, count: 28,
          statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 18, statusName: "On Hold Test", sortOrder: 2, count: 28,} ]},
        links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
      },{
        data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 14, statusName: "On Hold Zero", sortOrder: 2, count: 0,
          statusSummary:[]},
        links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
      }]};

 describe('Deal status reducer - ', () => {
    it('should return the default state', () => {
      const result = reducer(undefined, {} as any);
      expect(result).toBeTruthy();
    });
  });

  describe('Deal status reducers', () => {
    it('get deal status success', () => {
      
      const createAction = new GetDealStatusSuccessAction(dealsCount);
      const result = reducer(dealsList, createAction);

      expect(result.dealsCount[0].data).toEqual(expectedmockdealStatusResult.dealsCount[0].data);
      expect(result.dealsCount[0].data.statusCode).toEqual(expectedmockdealStatusResult.dealsCount[0].data.statusCode);
      expect(result.dealsCount[0].data.statusName).toEqual(expectedmockdealStatusResult.dealsCount[0].data.statusName);
    });

    it('get deal status success With Null Data', () => {  
      debugger      
        const expectedmockdealStatusResult: any={"dealsCount": []};
        const createAction = new GetDealStatusSuccessAction(dealsCount);
        const result = reducer(dealsCount, createAction);
        expect(result.dealsCount.length).toEqual(3);
      });

    it('update deal status actions when workflow id are same', () => {
      const dealsCount:any={"dealsCount":
       [ {
            data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
                   isCtrlKey: false,
                   statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
            links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
      }]}
      const payload: any = {
           data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
             isCtrlKey: false,
              statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
           links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
     }
      const expectedmockdealStatusResult: any={"dealsCount":
      [ {
           data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
              isCtrlKey: false,
              statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
           links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
     }]}
      const createAction = new UpdateDealStatusAction(payload);
      const result = reducer(dealsCount, createAction);
      expect(result).toEqual(expectedmockdealStatusResult);
      expect(result.dealsCount[0].data.workflowId).toEqual(expectedmockdealStatusResult.dealsCount[0].data.workflowId);
      expect(result.dealsCount[0].data.statusCode).toEqual(expectedmockdealStatusResult.dealsCount[0].data.statusCode);
      expect(result.dealsCount[0].data.statusName).toEqual(expectedmockdealStatusResult.dealsCount[0].data.statusName);
    });

    it('update sub deal status actions', () => {
      const dealsCount:any={"dealsCount":
      [ {
           data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
              statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
           links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
     }]}
     const payload:any= {"dealSubsSatus":{
      isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,}
     }
    const expectedmockdealStatusResult: any={"dealsCount":
    [ {
        data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 56,
            statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
        links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
    }]}

      const createAction = new UpdateSubDealStatusAction(payload);
      const result = reducer(dealsCount, createAction);
      expect(result).toEqual(expectedmockdealStatusResult);
      expect(result.dealsCount[0].data.statusCode).toEqual(expectedmockdealStatusResult.dealsCount[0].data.statusCode);
      expect(result.dealsCount[0].data.statusName).toEqual(expectedmockdealStatusResult.dealsCount[0].data.statusName);

    });

    it('update deal status actions when workflow id are same and Control key is pressed', () => {
      const dealsCount:any={"dealsCount":
        [ {
          data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
            isCtrlKey: true,
            statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
          links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
        },
          {
            data: {isDisabled:false ,workflowId:2,workflowName:"pqr",isSelected:true,statusCode: 17, statusName: "In Progress", sortOrder: 2, count: 28,
              isCtrlKey: false,
              statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
            links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
          }
        ]}
      const payload: any = {
        data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:false,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
          isCtrlKey: false,
          statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
        links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
      }
      const expectedmockdealStatusResult: any={"dealsCount":
        [ {
          data: {isDisabled:false ,workflowId:2,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
            isCtrlKey: false,
            statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
          links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
        }]}

      const createAction = new UpdateDealStatusAction(payload);
      const result = reducer(dealsCount, createAction);
      expect(result.dealsCount[0].data.statusCode).toEqual(expectedmockdealStatusResult.dealsCount[0].data.statusCode);
      expect(result.dealsCount[0].data.statusName).toEqual(expectedmockdealStatusResult.dealsCount[0].data.statusName);
    });

    it('update deal status actions when workflow id are different and Control key is pressed', () => {
      const dealsCount:any={"dealsCount":
        [ {
          data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
            isCtrlKey: true,
            statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
          links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
        },
          {
            data: {isDisabled:false ,workflowId:2,workflowName:"pqr",isSelected:true,statusCode: 17, statusName: "In Progress", sortOrder: 2, count: 28,
              isCtrlKey: false,
              statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
            links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
          },
          {
            data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 18, statusName: "In Progress", sortOrder: 2, count: 28,
              isCtrlKey: true,
              statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 18, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
            links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
          }
        ]}
      const payload: any = {
        data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
          isCtrlKey: false,
          statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
        links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
      }
      const expectedmockdealStatusResult: any={"dealsCount":
        [ {
          data: {isDisabled:false ,workflowId:2,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
            isCtrlKey: false,
            statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
          links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
        }]}

      const createAction = new UpdateDealStatusAction(payload);
      const result = reducer(dealsCount, createAction);

      expect(result.dealsCount[0].data.statusCode).toEqual(expectedmockdealStatusResult.dealsCount[0].data.statusCode);
      expect(result.dealsCount[0].data.statusName).toEqual(expectedmockdealStatusResult.dealsCount[0].data.statusName);
    });

    it('update deal status actions when deselecting panel if control key is not pressed', () => {
      const dealsCount:any={"dealsCount":
        [ {
          data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
            isCtrlKey: false,
            statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
          links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
        },
          {
            data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 18, statusName: "In Progress", sortOrder: 2, count: 28,
              isCtrlKey: false,
              statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 18, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
            links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
          },
          {
            data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 18, statusName: "Bound", sortOrder: 2, count: 28,
              isCtrlKey: false,
              statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 18, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
            links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
          }
        ]}
      const payload: any = {
        data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:false,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
          isCtrlKey: false,
          statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
        links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
      }
      const expectedmockdealStatusResult: any={"dealsCount":
        [ {
          data: {isDisabled:false ,workflowId:2,workflowName:"xyz",isSelected:false,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
            isCtrlKey: false,
            statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,} ]},
          links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
        }]}

      const createAction = new UpdateDealStatusAction(payload);
      const result = reducer(dealsCount, createAction);

      expect(result.dealsCount[0].data.statusCode).toEqual(expectedmockdealStatusResult.dealsCount[0].data.statusCode);
      expect(result.dealsCount[0].data.statusName).toEqual(expectedmockdealStatusResult.dealsCount[0].data.statusName);
    });
  });





