# BigIntegerManager
 방치형 게임을 만들때 무한에 가까운 수를 표현하기 위한 BigInteger 관련한 유틸리티 클래스 입니다.
 
- 너무 말도 안되게 큰 값을 주로 사용하는경우 (uint.MaxValue의 30제곱 이라던지)에는 한 함수 안에서 루프문으로 Divide를 해야하기에 성능 저하가 생길 수 있습니다.
- 가비지 컬렉션은 거의 발생하지 않으나. BigInteger에서는 Divide 연산이 많아지는 경우 가비지가 발생합니다. 
 
```cs
BigIntegerManager.GetUnit(10000); 
Result : 10A


BigIntegerManager.ToUnit("100Z")
Result : 100000000000000000000000000000000000000000000000000000000000000000000000000000000000
```
