using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SocialPlatforms.Impl;

[CustomEditor(typeof(ScoreAndRank))]
public class ScoreAndRankEditor : Editor {


    private ScoreAndRank scoreRank;

    private LevelScore levelScore;

    private bool hasLevelScorePath=false;
    private string levelScorePath;

    void OnEnable()
    {
        scoreRank = (ScoreAndRank)target;
    }

    public override void OnInspectorGUI()
    {     
        scoreRank.CurrLevelScore = (LevelScore)EditorGUILayout.ObjectField(
            "Current Level Score",
            scoreRank.CurrLevelScore, 
            typeof(LevelScore),false);
        if (scoreRank.CurrLevelScore)
        {
            if (!hasLevelScorePath)
            {
                levelScorePath = AssetDatabase.GetAssetPath(scoreRank.CurrLevelScore);
                hasLevelScorePath=true;
            }
            EditorGUILayout.LabelField(levelScorePath);

            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();

            scoreRank.CurrLevelScore.LevelName =
                EditorGUILayout.TextField("Level Name",
                scoreRank.CurrLevelScore.LevelName);

            if (EditorGUI.EndChangeCheck())
            {
                if (scoreRank.CurrLevelScore.name != scoreRank.CurrLevelScore.LevelName)
                    scoreRank.CurrLevelScore.name = scoreRank.CurrLevelScore.LevelName;
            }

            scoreRank.UsePercentages = EditorGUILayout.Toggle("Use Percentages", scoreRank.UsePercentages);

            if(scoreRank.UsePercentages)
            {
                EditorGUI.BeginChangeCheck();
                scoreRank.PercentMaxScore = EditorGUILayout.IntField("Percent Max Score"
                    , scoreRank.PercentMaxScore);
                scoreRank.BronzePercent = EditorGUILayout.Slider("Bronze", 
                    scoreRank.BronzePercent, 0, 1f);
                scoreRank.BronzePercent = Mathf.Clamp(scoreRank.BronzePercent, 0, scoreRank.SilverPercent);

                scoreRank.SilverPercent = EditorGUILayout.Slider("Silver",
                    scoreRank.SilverPercent, 0, 1f);
                scoreRank.SilverPercent = Mathf.Clamp(scoreRank.SilverPercent, scoreRank.BronzePercent, scoreRank.GoldPercent);

                scoreRank.GoldPercent = EditorGUILayout.Slider("Gold",
                   scoreRank.GoldPercent, 0, 1f);
                scoreRank.GoldPercent = Mathf.Clamp(scoreRank.GoldPercent,scoreRank.SilverPercent, 1);
                if(EditorGUI.EndChangeCheck())
                {
                    scoreRank.CalcPercentages();
                }

                EditorGUILayout.IntField("Bronze", scoreRank.CurrLevelScore.Bronze);
                EditorGUILayout.IntField("Silver", scoreRank.CurrLevelScore.Silver);
                EditorGUILayout.IntField("Gold", scoreRank.CurrLevelScore.Gold);
            }
            else
            {
                scoreRank.CurrLevelScore.Bronze =
                    EditorGUILayout.IntField("Bronze", scoreRank.CurrLevelScore.Bronze);
                scoreRank.CurrLevelScore.Silver =
                    EditorGUILayout.IntField("Silver", scoreRank.CurrLevelScore.Silver);
                scoreRank.CurrLevelScore.Gold =
                    EditorGUILayout.IntField("Gold", scoreRank.CurrLevelScore.Gold);
            }

            if (GUILayout.Button("Save Level Score"))
            {
                AssetDatabase.RenameAsset(levelScorePath, scoreRank.CurrLevelScore.LevelName);
                hasLevelScorePath = false;
                AssetDatabase.SaveAssets();
            }
        }
        else
            hasLevelScorePath = false;

        if(GUILayout.Button("Create New Level Score"))
        {
            LevelScore tempScore;
            tempScore = CreateInstance<LevelScore>();
            AssetDatabase.CreateAsset(tempScore, "Assets/Score&Ranking/LevelScores/NewLevel.asset");
            if(!scoreRank.CurrLevelScore)
                scoreRank.CurrLevelScore = tempScore;
            AssetDatabase.SaveAssets();
        }

    }
}
