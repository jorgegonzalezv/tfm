#!/bin/bash
# declare -a arr=("size_5" "size_10" "size_20" "size_50" "size_100")
declare -a arr=("size_5")

for i in "${arr[@]}"
do 
    echo "TRAINING :::: $i"
    CROSS=$i
    LIBPATH=lib/yolov5-master
    FROM=tfm_v4_unfrozen
    MODELPATH=$LIBPATH/runs/train/saved/$FROM/weights/best.pt
    DATASET=seadronesee_cross/$CROSS
    NAME=${FROM}_sds_${CROSS}_intermediate
    EPOCHS=10
    PATIENCE=10

    python $LIBPATH/train.py \
        --data datasets/$DATASET/data.yaml \
        --name $NAME \
        --weights $MODELPATH \
        --optimizer Adam \
        --freeze 10 \
        --patience $PATIENCE \
        --epochs $EPOCHS \
        --hyp $LIBPATH/data/hyps/hyp.VOC.yaml

    MODELPATH=$LIBPATH/runs/train/$NAME/weights/best.pt
    NAME=tfm_v4_unfrozen_sds_${CROSS} # dia_mes_datasettrained
    EPOCHS=20
    PATIENCE=20

    python $LIBPATH/train.py \
        --data datasets/$DATASET/data.yaml \
        --name $NAME \
        --weights $MODELPATH \
        --optimizer Adam \
        --patience $PATIENCE \
        --epochs $EPOCHS \
        --hyp $LIBPATH/data/hyps/hyp.VOC2.yaml 
done