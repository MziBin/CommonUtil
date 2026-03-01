# CommonUtil接口重构计划

## 1. 重构IXML接口

### 1.1 修改IXML接口
- 移除`FilePath`属性
- 为每个方法添加`filePath`参数作为第一个参数
- 更新方法注释

### 1.2 修改XMLHandlerToLINQImpl实现类
- 移除`_filePath`字段和`FilePath`属性
- 更新所有方法签名，添加`filePath`参数
- 移除`EnsureFileExists`方法，将其逻辑整合到各个方法中
- 移除构造函数的`filePath`参数
- 更新所有方法调用，传递正确的`filePath`参数

## 2. 为适合的接口添加静态包装类

### 2.1 JsonHelper
- 包装IJson接口，提供静态方法
- 内部使用TextJSONImpl实例
- 方法：`SerializeObject<T>`, `DeserializeObject<T>`, `GetValueFromJson`

### 2.2 DeepCopyHelper
- 包装IDeepCopy接口，提供静态方法
- 内部使用BinaryDeepCopyImpl实例
- 方法：`DeepCopy<T>`

### 2.3 IniHelper
- 包装IIni接口，提供静态方法
- 内部使用IniImpl实例
- 方法：所有IIni接口方法

### 2.4 WinSysHelper
- 包装IWinSys接口，提供静态方法
- 内部使用WinSysImpl实例
- 方法：`GetCurrentProcessMemoryUsage`, `GetSystemMemoryInfo`, `GetCpuUsage`

### 2.5 XmlHelper
- 包装IXML接口，提供静态方法
- 内部使用XMLHandlerToLINQImpl实例
- 方法：所有IXML接口方法（重构后的）

## 3. 保持现有接口和实现不变

### 3.1 ILog接口
- 已经实现了单例模式，保持不变
- BZLogImpl继续使用单例模式

### 3.2 ISCV接口
- 待实现，暂时不处理

## 4. 重构步骤

1. 首先重构IXML接口和XMLHandlerToLINQImpl实现类
2. 添加XmlHelper静态包装类
3. 依次添加JsonHelper、DeepCopyHelper、IniHelper、WinSysHelper静态包装类
4. 测试所有重构后的功能

## 5. 预期效果

- 接口设计更加一致，所有方法都接受完整的参数
- 调用方式更加简洁，不需要创建实例
- 保持了接口和实现的分离，便于扩展
- 适合static的接口提供了静态调用方式
- 适合单例的接口保持了单例模式
- 提高了代码的易用性、高效性和可维护性