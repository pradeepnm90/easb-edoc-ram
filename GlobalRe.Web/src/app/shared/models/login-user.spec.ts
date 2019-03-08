import {LoginUser} from './login-user';

// Model

// Spec
it('Should return login user model', () => {
  const item = new LoginUser({userName: "",
    applicationVersion:  "",
    domainName:  "",
    environment:  "",
    isAuthenticated: true})

  expect(item instanceof LoginUser).toBe(true, 'instance of login user');  
});