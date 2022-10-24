## Example

```c#
public class FileSystem : MonoBehaviour
{
    void Start()
    {
        CustomFileSystem f = new CustomFileSystem("deneme.txt");
        f.create();
        f.write("Hello");
        f.write(" World", true);

        Debug.Log(f.read());
    }
}
```

### Base64 Example

```c#
        CustomFileSystem f = new CustomFileSystem("deneme.txt");
        f.create();
        f.write("Hello", b64: true);
        f.write(" World", true, true);
        Debug.Log(f.read(true));     
```
