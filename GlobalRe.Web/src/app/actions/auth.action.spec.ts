import * as actions from './auth.action'

describe('actions', () => {

  it('should create an action ', () => {


    // execute
    let action = actions.LoginAction;

    const username = 'kdwivedi'
    const expectedAction = {
      type: actions.AuthActionTypes.LOGIN,
      username
    }    
   // expect(actions.LoginAction).toEqual('kdwivedi')
  })
})