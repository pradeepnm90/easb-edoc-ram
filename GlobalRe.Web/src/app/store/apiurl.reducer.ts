import { ApiUrlActions, ApiUrlActionTypes} from '../actions/apiurl.action';
import { ApiUrl } from '../shared/models/api-url';
export interface ApiUrlState {
  apiUrlList:ApiUrl[];
}

export const initialState: ApiUrlState = {
  apiUrlList:[]
};
export function reducer(state = initialState, action: ApiUrlActions): ApiUrlState {
  switch (action.type) {
    case ApiUrlActionTypes.ADD_APIURL: {
      action.payload.map(apiurl=>{
        const apiUrlbyKey = state.apiUrlList.find(url => url.key === apiurl.key);
        if (!(apiUrlbyKey))
        state.apiUrlList.push(apiurl);
      });
      return {...state};
    }
    case ApiUrlActionTypes.UPDATE_APIURL: {
      action.payload.map(apiurl => {
        const apiUrlbyKey = state.apiUrlList.find(url => url.key === apiurl.key);
        if (apiUrlbyKey)
          state.apiUrlList.find(url => url.key === apiurl.key).apiUrl = apiurl.apiUrl;
        else
        state.apiUrlList.push(apiurl);
      });
      return {...state};
    }
    default: {
      return state;
    }
  }
}
export const getApiUrls= (state: ApiUrlState) => state.apiUrlList;
