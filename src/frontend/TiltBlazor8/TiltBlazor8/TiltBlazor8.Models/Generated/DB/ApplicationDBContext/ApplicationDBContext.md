//modified 2023.12.15



# ApplicationDBContext

```mermaid
erDiagram
    TILT_Note {
      
      long ID 
      
      long IDURL 
      
      string Text 
      
      string Link 
      
      DateTime ForDate 
      
      string TimeZoneString 
    
    }
    TILT_Tag {
      
      long ID 
      
      string Name 
    
    }
    TILT_Tag_Note {
      
      long ID 
      
      long IDTag 
      
      long IDNote 
    
    }
    TILT_URL {
      
      long ID 
      
      string URLPart 
      
      string Secret 
    
    }
```
