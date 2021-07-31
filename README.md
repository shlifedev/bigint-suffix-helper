# BigIntegerManager
 방치형 게임을 만들때 무한에 가까운 수를 표현하기 위한 BigInteger 관련한 유틸리티 클래스 입니다. 


 소수점 1자리 지원 (1.2A == 1200), 무한에 가까운 수 지원, 가비지 최소화
 

```cs
BigIntegerManager.GetUnit(10000); 
Result : 10A
BigIntegerManager.GetUnit(1317); 
Result : 1.3A


BigIntegerManager.ToUnit("100Z")
Result : 100000000000000000000000000000000000000000000000000000000000000000000000000000000000



BigIntegerManager.ToUnit("1.5A")
Result : 1500
```
