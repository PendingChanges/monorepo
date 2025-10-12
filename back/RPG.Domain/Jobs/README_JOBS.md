# Système de Jobs - Documentation

## Vue d'ensemble

Le système de jobs permet aux personnages d'avoir plusieurs classes/métiers avec des conditions de déverrouillage. Chaque personnage commence avec 7 jobs, dont 2 sont débloqués par défaut.

## Jobs par défaut

Lorsqu'un personnage est créé, il reçoit automatiquement les 7 jobs suivants :

### 1. **Écuyer** (Ecuyer)
- ? **Débloqué par défaut**
- ? Aucune condition
- ?? Job de base pour les combattants physiques

### 2. **Chevalier**
- ?? **Verrouillé par défaut**
- ?? **Condition** : Écuyer niveau 3
- ?? Évolution du chemin du guerrier

### 3. **Alchimiste**
- ? **Débloqué par défaut**
- ? Aucune condition
- ?? Job de base pour les utilisateurs de magie

### 4. **Mage Blanc**
- ?? **Verrouillé par défaut**
- ?? **Condition** : Alchimiste niveau 3
- ?? Spécialisation dans la magie de soin

### 5. **Mage Noir**
- ?? **Verrouillé par défaut**
- ?? **Condition** : Alchimiste niveau 3
- ?? Spécialisation dans la magie offensive

### 6. **Archer**
- ?? **Verrouillé par défaut**
- ?? **Conditions multiples** :
  - Écuyer niveau 3 **ET**
  - Alchimiste niveau 2
- ?? Job hybride nécessitant les deux chemins de base

### 7. **Moine**
- ?? **Verrouillé par défaut**
- ?? **Condition** : Chevalier niveau 3
- ?? Job avancé du chemin physique

## Architecture du système

### Classes principales

#### `Job`
```csharp
public record Job
{
    public string Name { get; }
    public int Level { get; }
    public bool IsUnlocked { get; }
    public UnlockConditionCollection UnlockConditions { get; }
    // ...
}
```

#### `UnlockCondition`
```csharp
public record UnlockCondition
{
    public string JobName { get; }
    public int LevelRequired { get; }
    
    public bool IsSatisfiedBy(Job job) { ... }
}
```

#### `JobCollection`
```csharp
public class JobCollection
{
    public IReadOnlyList<Job> Jobs { get; }
    public Job? GetJobByName(string name) { ... }
    public bool HasJob(string name) { ... }
}
```

#### `DefaultJobFactory`
```csharp
public static class DefaultJobFactory
{
    public static JobCollection CreateDefaultJobs() { ... }
}
```

## Utilisation

### Créer un personnage avec les jobs par défaut
```csharp
var character = Character.CreateNew("Hero");

// Le personnage a automatiquement 7 jobs
Assert.Equal(7, character.Jobs.Jobs.Count);

// Deux jobs sont débloqués par défaut
var squire = character.Jobs.GetJobByName("Ecuyer");
Assert.True(squire.IsUnlocked);

var alchemist = character.Jobs.GetJobByName("Alchimiste");
Assert.True(alchemist.IsUnlocked);
```

### Vérifier les conditions de déverrouillage
```csharp
var character = Character.CreateNew("Hero");
var knight = character.Jobs.GetJobByName("Chevalier");

// Le Chevalier nécessite Écuyer niveau 3
Assert.Equal(1, knight.UnlockConditions.Conditions.Count);
Assert.Equal("Ecuyer", knight.UnlockConditions.Conditions[0].JobName);
Assert.Equal(3, knight.UnlockConditions.Conditions[0].LevelRequired);
```

### Jobs avec conditions multiples
```csharp
var archer = character.Jobs.GetJobByName("Archer");

// L'Archer nécessite 2 conditions
Assert.Equal(2, archer.UnlockConditions.Conditions.Count);

// Écuyer niveau 3 ET Alchimiste niveau 2
var conditions = archer.UnlockConditions.Conditions;
Assert.Contains(conditions, c => c.JobName == "Ecuyer" && c.LevelRequired == 3);
Assert.Contains(conditions, c => c.JobName == "Alchimiste" && c.LevelRequired == 2);
```

## Arbre de progression

```
                    ???????????
                    ?  Moine  ?
                    ? (Monk)  ?
                    ???????????
                         ?
                         ? Chevalier Lv3
                         ?
                    ????????????
                    ?Chevalier ?
                    ?(Knight)  ?
                    ????????????
                         ?
                         ? Écuyer Lv3
                         ?
    ???????????????????????????????????????????
    ?                                         ?
?????????                                 ????????????
?Écuyer ?                                 ?  Archer  ?
?(Squire)??????????????????????????????????? (Archer) ?
?????????                                 ????????????
  (Base)                                      ?
                                              ? Alchimiste Lv2
                                              ?
                                         ???????????????
                                         ?             ?
                                    ??????????    ??????????
                                    ? Mage   ?    ? Mage   ?
                                    ? Blanc  ?    ? Noir   ?
                                    ?(White) ?    ?(Black) ?
                                    ??????????    ??????????
                                         ?             ?
                                         ? Alchimiste Lv3
                                         ?             ?
                                    ????????????????????????
                                    ?    Alchimiste        ?
                                    ?   (Alchemist)        ?
                                    ????????????????????????
                                           (Base)
```

## Tests

Le système est couvert par 10 tests BDD (Reqnroll) :

1. ? A character is created with default jobs
2. ? Ecuyer and Alchimiste are unlocked by default
3. ? Other jobs are locked by default
4. ? Chevalier requires Ecuyer level 3
5. ? Mage Blanc requires Alchimiste level 3
6. ? Mage Noir requires Alchimiste level 3
7. ? Archer requires Ecuyer level 3 and Alchimiste level 2
8. ? Moine requires Chevalier level 3
9. ? A character can be created with default values
10. ? A character can be renamed

## Extension future

Le système peut être facilement étendu pour :
- Ajouter de nouveaux jobs
- Modifier les conditions de déverrouillage
- Implémenter la logique de déverrouillage automatique
- Gérer l'expérience et la montée de niveau des jobs
- Ajouter des compétences spécifiques à chaque job
