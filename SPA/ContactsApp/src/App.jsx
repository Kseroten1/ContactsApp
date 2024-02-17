import { useState, useEffect, useRef } from "react";
import "./App.css";
import instance from "./httpclient";
import Contact from "./components/Contact";
import Modal from "./components/Modal";

function App() {
  const [contacts, setContacts] = useState([]);
  const [categories, setCategories] = useState([]);
  const [detailedContact, setDetailedContact] = useState("");
  const [subCategories, setSubCategories] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isInfoModalOpen, setIsInfoModalOpen] = useState(false);
  const [isAddingModalOpen, setIsAddingModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [selectedCategory, setSelectedCategory] = useState("");
  const [info, setInfo] = useState("");
  const selectRef = useRef();
  useEffect(() => {
    instance
      .get("/test")
      .then(() => {
        setIsLoggedIn(true);
      })
      .catch(() => {});
    instance.get("/Contact").then((res) => {
      setContacts(res.data);
    });
    instance.get("/Contact/Category").then((res) => {
      setCategories(res.data);
    });
    instance.get("/Contact/Subcategory").then((res) => {
      setSubCategories(res.data);
    });
  }, []);

  const [isLoggedIn, setIsLoggedIn] = useState(false);

  function handleSubmitLogin(submitEvent) {
    submitEvent.preventDefault();
    const formData = new FormData(submitEvent.target);
    const data = Object.fromEntries(formData.entries());
    instance
      .post("/login", data, {
        params: { useCookies: true, useSessionCookies: true },
      })
      .then(() => {
        setIsLoggedIn(true);
        setInfo("User logged in");
      })
      .catch(() => {});
  }

  function handleSubmitEditingContact(submitEvent) {
    submitEvent.preventDefault();
    const formData = new FormData(submitEvent.target);
    const data = Object.fromEntries(formData.entries());
    console.log(data);
    instance.put("/Contact", data).then(() => {
      setIsEditModalOpen(false);
      setInfo("Contact Edited");
    });
  }

  function handleSubmitRegister(submitEvent) {
    submitEvent.preventDefault();
    const formData = new FormData(submitEvent.target);
    const data = Object.fromEntries(formData.entries());
    instance
      .post("/register", data, {
        params: { useCookies: true, useSessionCookies: true },
      })
      .then(() => {
        setIsModalOpen(false);
        setInfo("User registered");
      })
      .catch(() => {});
  }

  function logout() {
    instance.post("/logout", {}).then(() => {
      setInfo("Logged out");
      setIsLoggedIn(false);
    });
  }

  function getContactInfo(id) {
    instance.get("/Contact/" + id).then((res) => {
      setDetailedContact(res.data);
    });
  }

  function handleSubmitAddingContact(submitEvent) {
    submitEvent.preventDefault();
    const formData = new FormData(submitEvent.target);
    const data = Object.fromEntries(formData.entries());
    instance
      .post("/Contact", data)
      .then(() => {
        setInfo("Contact added");
        setIsAddingModalOpen(false);
      })
      .catch(() => {
        setInfo("Adding Contact unsuccessfull");
        setIsAddingModalOpen(false);
      });
  }

  function deleteContact(id) {
    instance.delete("/Contact/" + id).then(() => {
      setInfo("Contact Deleted");
    });
    instance.get("/Contact").then((res) => setContacts(res.data));
  }

  function SubCategorySwitch(props) {
    switch (props.categoryId) {
      case "1":
        return (
          <select name="subCategoryName">
            {subCategories
              .filter((subcategory) => subcategory.categoryId == 1)
              .map((subcategory) => (
                <option vaule={subcategory.name}>{subcategory.name}</option>
              ))}
          </select>
        );
      case "2":
        return (
          <>
            <input
              name="subCategoryName"
              style={{ display: "none" }}
              defaultValue="a"
            />
            <span>---</span>
          </>
        );
      case "3":
        return <input type="text" name="subCategoryName" />;
    }
  }

  return (
    <div style={{ display: "flex", flexFlow: "column", alignItems: "center" }}>
      {isLoggedIn ? (
        <></>
      ) : (
        <>
          <div style={{ display: "flex" }}>
            <form onSubmit={handleSubmitLogin}>
              <label>email: </label>
              <input name="email" type="text" />
              <label>password: </label>
              <input name="password" type="password" />
              <button type="submit">Log in</button>
            </form>
            <button onClick={() => setIsModalOpen(true)} type="button">
              Register
            </button>
          </div>
          <Modal
            isModalOpen={isModalOpen}
            closeModal={() => {
              setIsModalOpen(false);
            }}
          >
            <form onSubmit={handleSubmitRegister}>
              <label>email: </label>
              <input name="email" type="text" />
              <label>password: </label>
              <input name="password" type="password" />
              <button type="submit">Register</button>
            </form>
          </Modal>
        </>
      )}
      <div>{info}</div>
      {isLoggedIn ? (
        <>
          <button
            style={{ width: "15rem" }}
            onClick={() => setIsAddingModalOpen(true)}
          >
            Add Contact
          </button>
          <button style={{ width: "15rem" }} onClick={() => logout()}>
            Logout
          </button>
          <Modal
            isModalOpen={isAddingModalOpen}
            closeModal={() => setIsAddingModalOpen(false)}
          >
            <div
              style={{
                alignItems: "center",
                display: "flex",
                backgroundColor: "black",
              }}
            >
              <form
                onSubmit={handleSubmitAddingContact}
                style={{
                  display: "flex",
                  flexDirection: "column",
                  padding: "2rem",
                }}
              >
                <label>First Name: </label>
                <input name="firstName" type="text" />
                <label>Last Name: </label>
                <input name="lastName" type="text" />
                <label>Phone Number: </label>
                <input name="phoneNumber" type="text" />
                <label>Email: </label>
                <input name="email" type="text" />
                <label>Category: </label>
                <select
                  id="categorySelect"
                  name="categoryId"
                  ref={selectRef}
                  onChange={(e) => setSelectedCategory(e.target.value)}
                >
                  {categories.map((category) => (
                    <option value={category.categoryId}>{category.name}</option>
                  ))}
                </select>
                <label>SubCategory: </label>
                <SubCategorySwitch categoryId={selectedCategory} />
                <label>Date of birth: </label>
                <input name="birthDate" type="date" />
                <br></br>
                <button type="submit">Add</button>
              </form>
            </div>
          </Modal>
        </>
      ) : (
        <></>
      )}
      <Modal
        isModalOpen={isInfoModalOpen}
        closeModal={() => setIsInfoModalOpen(false)}
      >
        <div
          style={{
            alignItems: "center",
            padding: "2rem",
            backgroundColor: "black",
          }}
        >
          <p>{detailedContact.contactId}</p>
          <p>{detailedContact.firstName}</p>
          <p>{detailedContact.lastName}</p>
          <p>{detailedContact.phoneNumber}</p>
          <p>{detailedContact.email}</p>
          <p>{detailedContact.category}</p>
          <p>{detailedContact.subCategory}</p>
        </div>
      </Modal>
      <table>
        <thead>
          <tr>
            <th>Id</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Category</th>
          </tr>
        </thead>
        <tbody>
          {contacts.map((contact) => (
            <tr>
              <td>{contact.contactId}</td>
              <td>{contact.firstName}</td>
              <td>{contact.lastName}</td>
              <td>{contact.category}</td>
              {isLoggedIn ? (
                <>
                  <td>
                    <button
                      onClick={() => {
                        setIsInfoModalOpen(true);
                        getContactInfo(contact.contactId);
                      }}
                    >
                      Info
                    </button>
                  </td>
                  <td>
                    <Modal
                      isModalOpen={isEditModalOpen}
                      closeModal={() => setIsEditModalOpen(false)}
                    >
                      <div
                        style={{
                          alignItems: "center",
                          padding: "2rem",
                          backgroundColor: "black",
                        }}
                      >
                        <form
                          onSubmit={handleSubmitEditingContact}
                          style={{
                            display: "flex",
                            flexDirection: "column",
                            padding: "2rem",
                          }}
                        >
                          <label>First Name: </label>
                          <input
                            name="contactId"
                            value={contact.contactId}
                            readOnly={true}
                          ></input>
                          <input
                            name="firstName"
                            type="text"
                            placeholder={contact.firstName}
                          />
                          <label>Last Name: </label>
                          <input
                            name="lastName"
                            type="text"
                            placeholder={contact.lastName}
                          />
                          <label>Phone Number: </label>
                          <input
                            name="phoneNumber"
                            type="text"
                            placeholder={contact.phoneNumber}
                          />
                          <label>Email: </label>
                          <input
                            name="email"
                            type="text"
                            placeholder={contact.email}
                          />
                          <label>Category: </label>
                          <select
                            id="categorySelect"
                            name="categoryId"
                            placeholder={contact.categoryName}
                            ref={selectRef}
                            onChange={(e) =>
                              setSelectedCategory(e.target.value)
                            }
                          >
                            {categories.map((category) => (
                              <option value={category.categoryId}>
                                {category.name}
                              </option>
                            ))}
                          </select>
                          <label>SubCategory: </label>
                          <SubCategorySwitch categoryId={selectedCategory} />
                          <label>Date of birth: </label>
                          <input name="birthDate" type="date" />
                          <br></br>
                          <button type="submit">Edit</button>
                        </form>
                      </div>
                    </Modal>
                    <button
                      onClick={() => {
                        setIsEditModalOpen(true);
                      }}
                    >
                      Edit
                    </button>
                  </td>
                  <td>
                    <button onClick={() => deleteContact(contact.contactId)}>
                      Delete
                    </button>
                  </td>
                </>
              ) : (
                <></>
              )}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default App;
