
import {reducer} from './apiurl.reducer';
import * as fromApiUrl from './apiurl.reducer';
import { AddApiUrlAction,UpdateApiUrlAction } from '../actions/apiurl.action';
import { DEALSTATUSSUMMARIES_API_URL} from '../app.config';
import {ApiUrl} from '../shared/models/api-url';
import {EntityType} from "../app.config";

const _mockApiUrlList:ApiUrl[]=[new ApiUrl({key: EntityType.DealStatusSummaries, url: DEALSTATUSSUMMARIES_API_URL})];
       
describe('ApiUrl status reducer - ', () => {
  it('should return the default state', () => {
    const result = reducer(undefined, {} as any);
    expect(result).toBeTruthy();
  });
});

describe('ApiUrl reducers', () => {
  it('Add ApiUrl success', () => {
    const createAction = new AddApiUrlAction(_mockApiUrlList);
    const result = reducer(fromApiUrl.initialState, createAction);
  });

  it('update ApiUrl status actions', () => {
    const createAction = new UpdateApiUrlAction(_mockApiUrlList);
    const result = reducer({apiUrlList:_mockApiUrlList}, createAction);
    expect(result).toEqual({apiUrlList:_mockApiUrlList});
    expect(result.apiUrlList.length).toEqual(_mockApiUrlList.length);
  });
  it('update ApiUrl status actions', () => {
    let _mockApiUrlforOther:ApiUrl[]=[new ApiUrl({key: 'other', url: 'other'})]
    const createAction = new UpdateApiUrlAction(_mockApiUrlforOther);
    const result = reducer({apiUrlList:_mockApiUrlList}, createAction);
    expect(result).toEqual({apiUrlList:_mockApiUrlList});
    expect(result.apiUrlList.length).toEqual(_mockApiUrlList.length);
  });
 
});





