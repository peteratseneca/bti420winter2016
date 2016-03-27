### Week 10 code examples

Use the Visual Studio "Task List", and look for the "Attention" comment tokens.  

**PhotoProperty**

Handling images (as a way to learn about media types).  

Features:
- Capture (upload) a photo from a browser user
- Save it in the persistent store (database)
- Deliver it to a browser user

**PhotoEntity**

Design model class is associated one-to-many with a dedicated media item class.  

Features:
- Add a photo to an existing object, uses a combo add/edit pattern
- Custom details view, existing object, with its collection of photos
- As a bonus feature, photo download is supported 
