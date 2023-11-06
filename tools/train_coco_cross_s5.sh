#!/bin/bash
declare -a arr=("size_5" "size_10" "size_20" "size_50" "size_100")

for i in "${arr[@]}"
do 
    echo "TRAINING :::: $i"
    CROSS=$i
    LIBPATH=lib/yolov5-master
    MODELPATH=$LIBPATH/yolov5s.pt
    DATASET=seadronesee_cross_coco/$CROSS
    NAME=coco_pretrained_sds_${CROSS}_intermediate
    EPOCHS=10
    PATIENCE=10

    python $LIBPATH/train.py \
        --data datasets/$DATASET/data_coco.yaml \
        --name $NAME \
        --weights $LIBPATH/yolov5s.pt \
        --optimizer Adam \
        --freeze 10 \
        --patience $PATIENCE \
        --epochs $EPOCHS \
        --hyp $LIBPATH/data/hyps/hyp.VOC.yaml

    MODELPATH=$LIBPATH/runs/train/$NAME/weights/best.pt
    NAME=coco_pretrained_sds_${CROSS}
    EPOCHS=20
    PATIENCE=20

    python $LIBPATH/train.py \
        --data datasets/$DATASET/data_coco.yaml \
        --name $NAME \
        --weights $MODELPATH \
        --optimizer Adam \
        --patience $PATIENCE \
        --epochs $EPOCHS \
        --hyp $LIBPATH/data/hyps/hyp.VOC2.yaml 
done
