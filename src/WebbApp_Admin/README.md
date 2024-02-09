# React + TypeScript + Vite

This is a boilerplate for out Admin WebSite, 
It will be a site built on a Vite React Application.

## Features

### Users
Lists all users, klicking on edit or the name of the user, takes you to edit mode, where you can edit user details.

You will NOT be able to see the password, but you can choose to reset the password via the reset password button. 

You can DELETE a user, via the DELETE button, this step requires an extra security measure so admin's don't accidentaly delete users. 

### Items
Lists all items in Name order, 
Filtering and pagination will be implemented soon(tm)

Clickin on Edit or the Name will open the edit mode for that item. 
Delete will delete the item from the database, this will NOT have a security check before, so beware.

### Packlists
Lists all pakclist in ID order, 
Filtering and pagination will be implemented soon(tm)

Clickin on Edit or the Name will open the edit mode for that packlist. 
Delete will delete the item from the database, this will NOT have a security check before, so beware.

## Dev
Vite is a light weight fast render mode, to run Development mode, simple use: 

```js
npm run dev
``` 

#### React-Router
We are using React Router to route the site without reloading, this makes for simpler hanling of passing props between components and is done via the app.

```js
 <Navbar />
     <Container className="mb-4">
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/users" element={<Users />} />
        <Route path="/items" element={<Items />} />
        <Route path="/packlists" element={<Packlists />} />

      </Routes>
     </Container>
```

#### Axios
Axios uses a response promise, which mean we can handle the data as a model, whetever we've got it or not. 

Letting us list the data, or list none if no data.
This can be used if we wanna do a loader.
Might implement later, depending on the speed of the database.
```js 
export const getAllUsers = async () => {
    return await axios.get(apiUrl+`/api/Users`)
    .then(response => response.data)
}
```

More functionality to come, 
Enjoy

StuffPacker Team