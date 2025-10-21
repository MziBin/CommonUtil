# CommonUtil

用于编写.NetFormwork平台下面的通用接口工具，不带UI，因为UI不通用（winform和WPF其它的库文件都是通用的，只是UI不通用），注意要写接口文档和测试单元。

## 计划写的的工具

ini

Excel操作

CSV操作

json

xml

log日志：

    用自己的代码写一份，不用框架，这样方便有时快速使用。

数据库的增删改查：sql其实就是一个字符串，增删改返回一个bool判断是否成功，查返回一个dataset数据库类型。先简单的实现，后面再添加接口实现继承复杂的。

深度拷贝

序列化：可以参考别人写的，xml，Soap序列化，Binary序列化等序列化，还有json序列化

运算计时功能：



## 单元测试

#### 使用 MSTest 编写测试

MSTest是微软自带的，方便测试，但是只能是VisualStudio上使用。

#### 单元测试编写

1.编写一个每个测试方法都要的初始化步骤。

2.编写每一个方法测试完以后要清理的缓存文件

3.编写测试方法。

案例

```csharp
[TestClass]
public class IniTests
{
    private IIni _ini;
    private string _tempIniPath;

    /// <summary>
    /// 每个测试前初始化：创建临时 INI 文件，初始化 Ini 实例
    /// </summary>
    [TestInitialize]
    public void TestInitialize()
    {
        // 生成临时文件路径
        _tempIniPath = Path.GetTempFileName();
        // 初始化 Ini 对象并指定文件路径
        _ini = new Ini
        {
            FilePath = _tempIniPath
        };
    }

    /// <summary>
    /// 每个测试后清理：删除临时 INI 文件
    /// </summary>
    [TestCleanup]
    public void TestCleanup()
    {
        if (File.Exists(_tempIniPath))
        {
            File.Delete(_tempIniPath);
        }
    }

    #region 测试 WriteValue + ReadValue
    /// <summary>
    /// 测试：写入值后读取，结果应与写入值一致
    /// </summary>
    [TestMethod]
    public void WriteValue_And_ReadValue_ReturnsExpected()
    {
        // 准备：定义 section、key、value
        string section = "TestSection";
        string key = "TestKey";
        string value = "TestValue";

        // 执行：写入并读取值
        _ini.WriteValue(section, key, value);
        string result = _ini.ReadValue(section, key);

        // 断言：读取值应与写入值一致
        Assert.AreEqual(value, result, "读取值与写入值不一致");
    }

    /// <summary>
    /// 测试：键不存在时，读取应返回默认值
    /// </summary>
    [TestMethod]
    public void ReadValue_WithDefault_ReturnsDefaultWhenKeyNotExists()
    {
        // 准备：定义不存在的 section/key，以及默认值
        string section = "NonExistentSection";
        string key = "NonExistentKey";
        string defaultValue = "DefaultValue";

        // 执行：读取不存在的键（指定默认值）
        string result = _ini.ReadValue(section, key, defaultValue);

        // 断言：结果应等于默认值
        Assert.AreEqual(defaultValue, result, "未返回正确的默认值");
    }
    #endregion

    #region 测试 ReadKeys
    /// <summary>
    /// 测试：读取 section 下的所有键，应与预期列表一致
    /// </summary>
    [TestMethod]
    public void ReadKeys_ReturnsExpectedKeys()
    {
        // 准备：写入多个键到同一个 section
        string section = "KeysSection";
        List<string> expectedKeys = new List<string> { "Key1", "Key2", "Key3" };
        foreach (var key in expectedKeys)
        {
            _ini.WriteValue(section, key, "DummyValue");
        }

        // 执行：读取该 section 的所有键
        List<string> resultKeys = _ini.ReadKeys(section);

        // 断言：读取的键列表应与预期一致（不要求顺序，只要求元素等价）
        CollectionAssert.AreEquivalent(expectedKeys, resultKeys, "读取的键列表与预期不一致");
    }
    #endregion

    #region 测试 ReadSection
    /// <summary>
    /// 测试：读取整个 section 的键值对，应与预期字典一致
    /// </summary>
    [TestMethod]
    public void ReadSection_ReturnsExpectedDictionary()
    {
        // 准备：写入多个键值对到同一个 section
        string section = "SectionToRead";
        Dictionary<string, string> expectedDict = new Dictionary<string, string>
        {
            { "KeyA", "ValueA" },
            { "KeyB", "ValueB" }
        };
        foreach (var pair in expectedDict)
        {
            _ini.WriteValue(section, pair.Key, pair.Value);
        }

        // 执行：读取该 section 的所有键值对
        Dictionary<string, string> resultDict = _ini.ReadSection(section);

        // 断言：读取的字典应与预期一致（不要求顺序，只要求键值对等价）
        CollectionAssert.AreEquivalent(expectedDict, resultDict, "读取的键值对与预期不一致");
    }
    #endregion

    #region 测试 DeleteKey
    /// <summary>
    /// 测试：删除键后，读取应返回空值
    /// </summary>
    [TestMethod]
    public void DeleteKey_RemovesKey()
    {
        // 准备：写入一个键值对，然后删除该键
        string section = "DeleteSection";
        string key = "KeyToDelete";
        string value = "ValueToDelete";
        _ini.WriteValue(section, key, value);

        // 执行：删除键
        _ini.DeleteKey(section, key);
        string result = _ini.ReadValue(section, key);

        // 断言：读取结果应为空（表示键已被删除）
        Assert.AreEqual(string.Empty, result, "键未成功删除");
    }
    #endregion

    #region 测试 DeleteSection
    /// <summary>
    /// 测试：删除 section 后，该 section 下应无任何键
    /// </summary>
    [TestMethod]
    public void DeleteSection_RemovesSection()
    {
        // 准备：写入一个 section 及其键值对，然后删除该 section
        string section = "SectionToDelete";
        string key = "KeyInSection";
        string value = "ValueInSection";
        _ini.WriteValue(section, key, value);

        // 执行：删除 section
        _ini.DeleteSection(section);
        List<string> keys = _ini.ReadKeys(section);

        // 断言：该 section 下的键数量应为 0（表示 section 已被删除）
        Assert.AreEqual(0, keys.Count, "Section 未成功删除");
    }
    #endregion

    #region 测试 HasSection
    /// <summary>
    /// 测试：存在的 section 应返回 true
    /// </summary>
    [TestMethod]
    public void HasSection_ReturnsTrueWhenSectionExists()
    {
        // 准备：写入一个 section（即使只有一个虚拟键）
        string section = "ExistingSection";
        _ini.WriteValue(section, "DummyKey", "DummyValue");

        // 执行：判断 section 是否存在
        bool result = _ini.HasSection(section);

        // 断言：应返回 true
        Assert.IsTrue(result, "存在的 Section 未被正确识别");
    }

    /// <summary>
    /// 测试：不存在的 section 应返回 false
    /// </summary>
    [TestMethod]
    public void HasSection_ReturnsFalseWhenSectionNotExists()
    {
        // 准备：定义不存在的 section
        string section = "NonExistingSection";

        // 执行：判断 section 是否存在
        bool result = _ini.HasSection(section);

        // 断言：应返回 false
        Assert.IsFalse(result, "不存在的 Section 被错误识别为存在");
    }
    #endregion

    #region 测试 HasKey
    /// <summary>
    /// 测试：存在的键应返回 true
    /// </summary>
    [TestMethod]
    public void HasKey_ReturnsTrueWhenKeyExists()
    {
        // 准备：写入一个键值对
        string section = "KeySection";
        string key = "ExistingKey";
        _ini.WriteValue(section, key, "Value");

        // 执行：判断键是否存在
        bool result = _ini.HasKey(section, key);

        // 断言：应返回 true
        Assert.IsTrue(result, "存在的 Key 未被正确识别");
    }

    /// <summary>
    /// 测试：不存在的键应返回 false
    /// </summary>
    [TestMethod]
    public void HasKey_ReturnsFalseWhenKeyNotExists()
    {
        // 准备：定义不存在的键
        string section = "KeySection";
        string key = "NonExistingKey";

        // 执行：判断键是否存在
        bool result = _ini.HasKey(section, key);

        // 断言：应返回 false
        Assert.IsFalse(result, "不存在的 Key 被错误识别为存在");
    }
    #endregion
}
```

## 思想

接口通用方法：接口都应该有init和dispose这两个方法，用于初始化的调用和结束时的销毁

每个保存或者读取的方法都应该有路径这个参数，这样才能更加通用和迁移
