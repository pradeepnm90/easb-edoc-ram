
// import {reducer} from './auth.reducer';
// import * as fromAuth from './auth.reducer';
// import {AuthenticateSuccessAction, LoginAction, LoginSuccessAction, UpdateUserDisplayNameAction} from '../actions/auth.action';
// import{LoginUser} from '../shared/models/login-user';
// import {UpdateApiUrlAction} from "../actions/apiurl.action";

// const user:LoginUser= { userName:"kdwivedi", personId: 1234,access_token:"abc",
//                               applicationVersion:"123",domainName:"41",
//                               environment:"1",isAuthenticated:true,displayName:'KDwivedi',canEditDeal:true};
                              
// const expectedResult = {  loggedIn: true,  user: { name: 'kdwivedi' ,isAuthenticated:true,canEditDeal:true}, };                              

// describe('Authentication reducer -', () => {
//     it('should return the default state', () => {
//       const result = reducer(undefined, {} as any);
//       expect(result).toBeTruthy();
//     });
//   });

//   describe('Login Success', () => {
//     it('should add a user set loggedIn to true in auth state', () => {           
//       const createAction = new LoginSuccessAction({ user:user });     
//       const result = reducer(fromAuth.initialState, createAction);
//       expect(result.loggedIn).toEqual(expectedResult.loggedIn);
//       expect(result.user.userName).toEqual(expectedResult.user.name);
//     });
//     it('update user dispaly name actions', () => {    
//       const createAction = new UpdateUserDisplayNameAction({'userName': user.userName, 'personId': user.personId});           
//       const result = reducer({user:user,loggedIn:true}, createAction);
//       expect(result).toEqual({user:user,loggedIn:true});
//     });
//     it('update user dispaly name actions username is null', () => {      
//        const createAction = new UpdateUserDisplayNameAction({'userName': null, 'personId': 0});            
//        const result = reducer({user:user,loggedIn:false}, createAction);
//        expect(result).toEqual({user:user,loggedIn:false});
//      });
//     it('update authenticated success actions', () => {      
//       const createAction = new AuthenticateSuccessAction( { user:user});
//       const result = reducer({user:user,loggedIn:true}, createAction);
//       expect(result).toEqual({user:user,loggedIn:true});
//     });
//   });





